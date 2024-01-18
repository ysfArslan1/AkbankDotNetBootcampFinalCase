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

public class DocumentQueryHandler :
    IRequestHandler<GetAllDocumentQuery, ApiResponse<List<DocumentResponse>>>,
    IRequestHandler<GetDocumentByIdQuery, ApiResponse<DocumentResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DocumentQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Document sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<DocumentResponse>>> Handle(GetAllDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Document>().Where(x=> x.IsActive == true)
            .ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<DocumentResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Document>, List<DocumentResponse>>(list);
         return new ApiResponse<List<DocumentResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Document deðerlerinin alýndýðý query
    public async Task<ApiResponse<DocumentResponse>> Handle(GetDocumentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Document>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<DocumentResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Document, DocumentResponse>(entity);
        return new ApiResponse<DocumentResponse>(mapped);
    }

}