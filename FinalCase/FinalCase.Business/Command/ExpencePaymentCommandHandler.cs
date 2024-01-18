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
        

        var entity = mapper.Map<CreateExpencePaymentRequest, ExpencePayment>(request.Model);
        
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
        fromdb.IsDeposited = request.Model.IsDeposited;

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