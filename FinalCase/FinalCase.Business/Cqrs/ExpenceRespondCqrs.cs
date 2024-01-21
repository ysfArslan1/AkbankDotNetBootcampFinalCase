using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceRespondCommand(int CurrentUserId, CreateExpenceRespondRequest Model) : IRequest<ApiResponse<ExpenceRespondResponse>>;
public record UpdateExpenceRespondCommand(int Id, int CurrentUserId, UpdateExpenceRespondRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceRespondCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceRespondQuery() : IRequest<ApiResponse<List<ExpenceRespondResponse>>>;
public record GetExpenceRespondByIdQuery(int Id) : IRequest<ApiResponse<ExpenceRespondResponse>>;

// Employee
public record GetAllMyExpenceRespondQuery(int CurrentUserId) : IRequest<ApiResponse<List<ExpenceRespondResponse>>>;
public record GetMyExpenceRespondByIdQuery(int Id, int CurrentUserId) : IRequest<ApiResponse<ExpenceRespondResponse>>;
