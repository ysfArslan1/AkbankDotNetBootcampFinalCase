using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateDocumentCommand(int CurrentUserId, CreateDocumentRequest Model) : IRequest<ApiResponse<DocumentResponse>>;
public record UpdateDocumentCommand(int Id, int CurrentUserId, UpdateDocumentRequest Model) : IRequest<ApiResponse>;
public record DeleteDocumentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllDocumentQuery() : IRequest<ApiResponse<List<DocumentResponse>>>;
public record GetDocumentByIdQuery(int Id) : IRequest<ApiResponse<DocumentResponse>>;

// Employee
public record UpdateMyDocumentCommand(int Id, int CurrentUserId, UpdateDocumentRequest Model) : IRequest<ApiResponse>;
public record DeleteMyDocumentCommand(int Id, int CurrentUserId) : IRequest<ApiResponse>;

public record GetAllMyDocumentQuery(int CurrentUserId) : IRequest<ApiResponse<List<DocumentResponse>>>;
public record GetMyDocumentByIdQuery(int Id, int CurrentUserId) : IRequest<ApiResponse<DocumentResponse>>;
