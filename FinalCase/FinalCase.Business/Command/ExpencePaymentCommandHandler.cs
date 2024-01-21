using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class ExpencePaymentCommandHandler :
    IRequestHandler<CreateExpencePaymentCommand, ApiResponse<ExpencePaymentResponse>>,
    IRequestHandler<UpdateExpencePaymentCommand,ApiResponse>,
    IRequestHandler<DeleteExpencePaymentCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpencePaymentCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpencePayment sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<ExpencePaymentResponse>> Handle(CreateExpencePaymentCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<ExpencePayment>().Where(x => x.ExpenceRespondId == request.Model.ExpenceRespondId )
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<ExpencePaymentResponse>($"{request.Model.ExpenceRespondId} is used by another ExpencePayment.");
        }

        var Respond = await dbContext.Set<ExpenceRespond>().Where(x => x.Id == request.Model.ExpenceRespondId && x.isApproved==true)
            .FirstOrDefaultAsync(cancellationToken);
        if (Respond == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("ExpenceRespond not Aproved");
        }

        var Account = await dbContext.Set<Account>().Where(x => x.Id == request.Model.AccountId)
            .FirstOrDefaultAsync(cancellationToken);
        if (Account == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("Account not found");
        }

        var ReceiverAccount = await dbContext.Set<Account>().Where(x => x.IBAN == request.Model.ReceiverIban)
            .FirstOrDefaultAsync(cancellationToken);
        if (ReceiverAccount == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("Receiver Account not found");
        }

        if (ReceiverAccount.CurrencyType != Account.CurrencyType)
        {
            return new ApiResponse<ExpencePaymentResponse>($"{Account.Name} Account Currency Type, {request.Model.ReceiverIban}' Account  Currency Type not same");
        }

        var expenceNotify = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == Respond.ExpenceNotifyId)
            .FirstOrDefaultAsync(cancellationToken);
        if (expenceNotify == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("Expence Notify not found");
        }

        if (Account.Balance < expenceNotify.Amount)
        {
            return new ApiResponse<ExpencePaymentResponse>("Account Balance not enough");
        }

        Account.Balance = Account.Balance - expenceNotify.Amount;
        Account.UpdateUserId = request.CurrentUserId;
        Account.UpdateDate = DateTime.Now;

        ReceiverAccount.Balance = ReceiverAccount.Balance + expenceNotify.Amount;
        ReceiverAccount.UpdateUserId = request.CurrentUserId;
        ReceiverAccount.UpdateDate = DateTime.Now;

        Respond.IsDeposited= true;
        Respond.UpdateUserId = request.CurrentUserId;
        Respond.UpdateDate = DateTime.Now;

        dbContext.Accounts.Update(Account);
        dbContext.Accounts.Update(ReceiverAccount);
        dbContext.ExpenceResponds.Update(Respond);

        var entity = mapper.Map<CreateExpencePaymentRequest, ExpencePayment>(request.Model);
        entity.Description = $"{expenceNotify.Amount} {Account.CurrencyType} was sent from account {Account.Name} to {ReceiverAccount.Name}";
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpencePayment, ExpencePaymentResponse>(entityResult.Entity);
        return new ApiResponse<ExpencePaymentResponse>(mapped);
    }

    // ExpencePayment sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateExpencePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpencePayment>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Description = request.Model.Description;
        fromdb.TransactionDate = request.Model.TransactionDate;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpencePayment sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteExpencePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpencePayment>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete iþlemi yapýlýr
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}