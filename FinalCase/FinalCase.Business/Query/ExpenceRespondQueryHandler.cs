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

public class ExpenceRespondQueryHandler :
    IRequestHandler<GetAllExpenceRespondQuery, ApiResponse<List<ExpenceRespondResponse>>>,
    IRequestHandler<GetExpenceRespondByIdQuery, ApiResponse<ExpenceRespondResponse>>,
    IRequestHandler<GetAllMyExpenceRespondQuery, ApiResponse<List<ExpenceRespondResponse>>>,
    IRequestHandler<GetMyExpenceRespondByIdQuery, ApiResponse<ExpenceRespondResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceRespondQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceRespond s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpenceRespondResponse>>> Handle(GetAllExpenceRespondQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenceRespond>().Where(x=> x.IsActive == true)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpenceRespondResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpenceRespond>, List<ExpenceRespondResponse>>(list);
         return new ApiResponse<List<ExpenceRespondResponse>>(mappedList);
    }


    // �d de�eri ile istenilen ExpenceRespond de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpenceRespondResponse>> Handle(GetExpenceRespondByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<ExpenceRespond>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpenceRespondResponse>("Record not found");
        }
        
        var mapped = mapper.Map<ExpenceRespond, ExpenceRespondResponse>(entity);
        return new ApiResponse<ExpenceRespondResponse>(mapped);
    }

    // Employee

    // ExpenceRespond s�n�f�n�n database i�erisinde bulunan verilerinin al�nd�g� query
    public async Task<ApiResponse<List<ExpenceRespondResponse>>> Handle(GetAllMyExpenceRespondQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenceRespond>().Where(x => x.IsActive == true && x.UserId == request.CurrentUserId)
            .Include(x => x.User).ToListAsync(cancellationToken);

        // de�erin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpenceRespondResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpenceRespond>, List<ExpenceRespondResponse>>(list);
        return new ApiResponse<List<ExpenceRespondResponse>>(mappedList);
    }


    // �d de�eri ile istenilen ExpenceRespond de�erlerinin al�nd��� query
    public async Task<ApiResponse<ExpenceRespondResponse>> Handle(GetMyExpenceRespondByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<ExpenceRespond>()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive == true && x.UserId == request.CurrentUserId, cancellationToken);

        // de�erin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpenceRespondResponse>("Record not found");
        }

        var mapped = mapper.Map<ExpenceRespond, ExpenceRespondResponse>(entity);
        return new ApiResponse<ExpenceRespondResponse>(mapped);
    }
}