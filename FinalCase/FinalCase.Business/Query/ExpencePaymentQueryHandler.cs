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

public class ExpencePaymentQueryHandler :
    IRequestHandler<GetAllExpencePaymentQuery, ApiResponse<List<ExpencePaymentResponse>>>,
    IRequestHandler<GetExpencePaymentByIdQuery, ApiResponse<ExpencePaymentResponse>>,
    IRequestHandler<GetAllMyExpencePaymentQuery, ApiResponse<List<ExpencePaymentResponse>>>,
    IRequestHandler<GetMyExpencePaymentByIdQuery, ApiResponse<ExpencePaymentResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpencePaymentQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpencePayment s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpencePaymentResponse>>> Handle(GetAllExpencePaymentQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpencePayment>().Where(x=> x.IsActive == true)
            .Include(x => x.Account).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpencePaymentResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpencePayment>, List<ExpencePaymentResponse>>(list);
         return new ApiResponse<List<ExpencePaymentResponse>>(mappedList);
    }

    // �d de�eri ile istenilen ExpencePayment de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpencePaymentResponse>> Handle(GetExpencePaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<ExpencePayment>()
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("Record not found");
        }
        
        var mapped = mapper.Map<ExpencePayment, ExpencePaymentResponse>(entity);
        return new ApiResponse<ExpencePaymentResponse>(mapped);
    }

    // Employee

    // ExpencePayment s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpencePaymentResponse>>> Handle(GetAllMyExpencePaymentQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpencePayment>().Where(x => x.IsActive == true && x.ExpenceRespond.UserId == request.CurrentUserId)
            .Include(x => x.Account).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpencePaymentResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpencePayment>, List<ExpencePaymentResponse>>(list);
        return new ApiResponse<List<ExpencePaymentResponse>>(mappedList);
    }

    // �d de�eri ile istenilen ExpencePayment de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpencePaymentResponse>> Handle(GetMyExpencePaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ExpencePayment>()
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == true && x.ExpenceRespond.UserId == request.CurrentUserId, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpencePaymentResponse>("Record not found");
        }

        var mapped = mapper.Map<ExpencePayment, ExpencePaymentResponse>(entity);
        return new ApiResponse<ExpencePaymentResponse>(mapped);
    }
}