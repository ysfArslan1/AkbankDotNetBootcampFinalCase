using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class ExpenceNotifyCommandHandler :
    IRequestHandler<CreateExpenceNotifyCommand, ApiResponse<ExpenceNotifyResponse>>,
    IRequestHandler<UpdateExpenceNotifyCommand,ApiResponse>,
    IRequestHandler<DeleteExpenceNotifyCommand,ApiResponse>,
    IRequestHandler<UpdateMyExpenceNotifyCommand, ApiResponse>,
    IRequestHandler<DeleteMyExpenceNotifyCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceNotifyCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceNotify sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<ExpenceNotifyResponse>> Handle(CreateExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var checkUser = await dbContext.Set<User>().Where(x => x.Id == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkUser == null)
        {
            return new ApiResponse<ExpenceNotifyResponse>("User not found");
        }

        var check = await dbContext.Set<ExpenceType>().Where(x => x.Id == request.Model.ExpenceTypeId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse<ExpenceNotifyResponse>("ExpenceType not found");
        }

        var entity = mapper.Map<CreateExpenceNotifyRequest, ExpenceNotify>(request.Model);
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenceNotify, ExpenceNotifyResponse>(entityResult.Entity);
        return new ApiResponse<ExpenceNotifyResponse>(mapped);
    }

    // ExpenceNotify sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var check = await dbContext.Set<ExpenceType>().Where(x => x.Id == request.Model.ExpenceTypeId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse("ExpenceType not found");
        }

        fromdb.ExpenceTypeId = request.Model.ExpenceTypeId;
        fromdb.Explanation = request.Model.Explanation;
        fromdb.Amount = request.Model.Amount;
        fromdb.TransferType = request.Model.TransferType;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceNotify sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id)
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

    // Employee

    // ExpenceNotify sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateMyExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var check = await dbContext.Set<ExpenceType>().Where(x => x.Id == request.Model.ExpenceTypeId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse("ExpenceType not found");
        }

        fromdb.ExpenceTypeId = request.Model.ExpenceTypeId;
        fromdb.Explanation = request.Model.Explanation;
        fromdb.Amount = request.Model.Amount;
        fromdb.TransferType = request.Model.TransferType;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceNotify sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteMyExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
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