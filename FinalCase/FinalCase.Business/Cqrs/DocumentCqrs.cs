using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateDocumentCommand(DocumentRequest Model) : IRequest<ApiResponse<DocumentResponse>>;
public record UpdateDocumentCommand(int Id,DocumentRequest Model) : IRequest<ApiResponse>;
public record DeleteDocumentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllDocumentQuery() : IRequest<ApiResponse<List<DocumentResponse>>>;
public record GetDocumentByIdQuery(int Id) : IRequest<ApiResponse<DocumentResponse>>;
