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
    IRequestHandler<DeleteDocumentCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DocumentCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Document s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<DocumentResponse>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Model.ExpenceNotifyId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse<DocumentResponse>("ExpenceNotify not found");
        }

        var entity = mapper.Map<CreateDocumentRequest, Document>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Document, DocumentResponse>(entityResult.Entity);
        return new ApiResponse<DocumentResponse>(mapped);
    }

    // Document s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Description = request.Model.Description;
        fromdb.Content = request.Model.Content;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Document s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Document>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete i�lemi yap�l�r
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}