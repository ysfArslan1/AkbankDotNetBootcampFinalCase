using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class DocumentCommandHandler :
    IRequestHandler<CreateDocumentCommand, ApiResponse<DocumentResponse>>,
    IRequestHandler<UpdateDocumentCommand,ApiResponse>,
    IRequestHandler<DeleteDocumentCommand,ApiResponse>,
    IRequestHandler<UpdateMyDocumentCommand, ApiResponse>,
    IRequestHandler<DeleteMyDocumentCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DocumentCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Document sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<DocumentResponse>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Model.ExpenceNotifyId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse<DocumentResponse>("ExpenceNotify not found");
        }

        var entity = mapper.Map<CreateDocumentRequest, Document>(request.Model);
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;


        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Document, DocumentResponse>(entityResult.Entity);
        return new ApiResponse<DocumentResponse>(mapped);
    }

    // Document sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Description = request.Model.Description;
        fromdb.Content = request.Model.Content;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Document sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id)
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

    // Document sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateMyDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id && x.ExpenceNotify.UserId == request.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        fromdb.Description = request.Model.Description;
        fromdb.Content = request.Model.Content;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Document sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteMyDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id && x.ExpenceNotify.UserId == request.CurrentUserId)
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