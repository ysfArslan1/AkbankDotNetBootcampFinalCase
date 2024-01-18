using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceRespondCommand(CreateExpenceRespondRequest Model) : IRequest<ApiResponse<ExpenceRespondResponse>>;
public record UpdateExpenceRespondCommand(int Id, UpdateExpenceRespondRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceRespondCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceRespondQuery() : IRequest<ApiResponse<List<ExpenceRespondResponse>>>;
public record GetExpenceRespondByIdQuery(int Id) : IRequest<ApiResponse<ExpenceRespondResponse>>;
