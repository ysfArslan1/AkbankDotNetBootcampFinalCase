using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceRespondCommand(ExpenceRespondRequest Model) : IRequest<ApiResponse<ExpenceRespondResponse>>;
public record UpdateExpenceRespondCommand(int Id,ExpenceRespondRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceRespondCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceRespondQuery() : IRequest<ApiResponse<List<ExpenceRespondResponse>>>;
public record GetExpenceRespondByIdQuery(int Id) : IRequest<ApiResponse<ExpenceRespondResponse>>;
