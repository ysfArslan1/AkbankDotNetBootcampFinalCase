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

public class ExpenceNotifyQueryHandler :
    IRequestHandler<GetAllExpenceNotifyQuery, ApiResponse<List<ExpenceNotifyResponse>>>,
    IRequestHandler<GetExpenceNotifyByIdQuery, ApiResponse<ExpenceNotifyResponse>>,
    IRequestHandler<GetAllMyExpenceNotifyQuery, ApiResponse<List<ExpenceNotifyResponse>>>,
    IRequestHandler<GetMyExpenceNotifyByIdQuery, ApiResponse<ExpenceNotifyResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceNotifyQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceNotify s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpenceNotifyResponse>>> Handle(GetAllExpenceNotifyQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenceNotify>().Where(x=> x.IsActive == true)
            .Include(x=>x.ExpenceType)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpenceNotifyResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpenceNotify>, List<ExpenceNotifyResponse>>(list);
         return new ApiResponse<List<ExpenceNotifyResponse>>(mappedList);
    }

    // �d de�eri ile istenilen ExpenceNotify de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpenceNotifyResponse>> Handle(GetExpenceNotifyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<ExpenceNotify>()
            .Include(x => x.User)
            .Include(x => x.ExpenceType)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpenceNotifyResponse>("Record not found");
        }
        
        var mapped = mapper.Map<ExpenceNotify, ExpenceNotifyResponse>(entity);
        return new ApiResponse<ExpenceNotifyResponse>(mapped);
    }

    // Employee

    // ExpenceNotify s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpenceNotifyResponse>>> Handle(GetAllMyExpenceNotifyQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenceNotify>().Where(x => x.IsActive == true && x.UserId == request.CurrentUserId)
            .Include(x => x.ExpenceType)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpenceNotifyResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpenceNotify>, List<ExpenceNotifyResponse>>(list);
        return new ApiResponse<List<ExpenceNotifyResponse>>(mappedList);
    }

    // �d de�eri ile istenilen ExpenceNotify de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpenceNotifyResponse>> Handle(GetMyExpenceNotifyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ExpenceNotify>()
            .Include(x => x.User)
            .Include(x => x.ExpenceType)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == true && x.UserId == request.CurrentUserId, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpenceNotifyResponse>("Record not found");
        }

        var mapped = mapper.Map<ExpenceNotify, ExpenceNotifyResponse>(entity);
        return new ApiResponse<ExpenceNotifyResponse>(mapped);
    }
}