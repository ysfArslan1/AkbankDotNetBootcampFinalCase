using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Query;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>, 
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>,
    IRequestHandler<GetAccountByAccountNumberQuery, ApiResponse<AccountResponse>>,
    IRequestHandler<GetAllMyAccountQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetMyAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public AccountQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Account sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>().Where(x=> x.IsActive == true)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<AccountResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
         return new ApiResponse<List<AccountResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Account deðerlerinin alýndýðý query
    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Account>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<AccountResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByAccountNumberQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber && x.IsActive == true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<AccountResponse>("Record not found");
        }

        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    // Employee

    // Account sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllMyAccountQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>().Where(x => x.IsActive == true && x.UserId == request.CurrentUserId)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<AccountResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Account deðerlerinin alýndýðý query
    public async Task<ApiResponse<AccountResponse>> Handle(GetMyAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == true && x.UserId == request.CurrentUserId, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<AccountResponse>("Record not found");
        }

        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

}