using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class AccountCommandHandler :
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<UpdateAccountCommand,ApiResponse>,
    IRequestHandler<DeleteAccountCommand,ApiResponse>,
    IRequestHandler<UpdateMyAccountCommand, ApiResponse>,
    IRequestHandler<DeleteMyAccountCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public AccountCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Account sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Account>().Where(x => x.UserId == request.Model.UserId && x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<AccountResponse>($"{request.Model.Name} is used by another Account.");
        }
        var checkUser = await dbContext.Set<User>().Where(x => x.Id == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkUser == null)
        {
            return new ApiResponse<AccountResponse>("User not found");
        }

        var entity = mapper.Map<CreateAccountRequest, Account>(request.Model);

        // Iban ve Account numarasý oluþtur
        entity.AccountNumber =await generateAccountNumber();
        entity.IBAN =await generateIBAN();
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Account, AccountResponse>(entityResult.Entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    // Account sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Account>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        // Kullanýcýnýn ayný isimde baþka hesabý olup olmadýðý kontrol edilir.
        var check = await dbContext.Set<Account>().Where(x => x.UserId == fromdb.UserId && x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.Name} is used by another Account.");
        }

        fromdb.Name = request.Model.Name;
        fromdb.Balance = request.Model.Balance;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Account sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Account>().Where(x => x.Id == request.Id)
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

    public async Task<int> generateAccountNumber()
    {
        int AccountNumber = new Random().Next(1000000, 9999999);
        var checkAccount = await dbContext.Accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefaultAsync();

        if (checkAccount != null)
        {
            AccountNumber = new Random().Next(1000000, 9999999);
            checkAccount = await dbContext.Accounts.Where(x => x.AccountNumber == AccountNumber).FirstOrDefaultAsync();
        }
        return AccountNumber;
    }
    public async Task<string> generateIBAN()
    {
        string IBAN = new Random().Next(1000000, 9999999).ToString();
        var checkIBAN = await dbContext.Accounts.Where(x => x.IBAN == IBAN).FirstOrDefaultAsync();

        if (checkIBAN != null)
        {
            IBAN = new Random().Next(1000000, 9999999).ToString();
            checkIBAN = await dbContext.Accounts.Where(x => x.IBAN == IBAN).FirstOrDefaultAsync();
        }
        return IBAN;
    }


    // Employee


    // Account sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateMyAccountCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Account>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        // Kullanýcýnýn ayný isimde baþka hesabý olup olmadýðý kontrol edilir.
        var check = await dbContext.Set<Account>().Where(x => x.UserId == fromdb.UserId && x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.Name} is used by another Account.");
        }

        fromdb.Name = request.Model.Name;
        fromdb.Balance = request.Model.Balance;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Account sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteMyAccountCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Account>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
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