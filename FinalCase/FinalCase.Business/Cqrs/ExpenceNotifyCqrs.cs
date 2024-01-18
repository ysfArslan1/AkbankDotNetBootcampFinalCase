using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceNotifyCommand(CreateExpenceNotifyRequest Model) : IRequest<ApiResponse<ExpenceNotifyResponse>>;
public record UpdateExpenceNotifyCommand(int Id, UpdateExpenceNotifyRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceNotifyCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceNotifyQuery() : IRequest<ApiResponse<List<ExpenceNotifyResponse>>>;
public record GetExpenceNotifyByIdQuery(int Id) : IRequest<ApiResponse<ExpenceNotifyResponse>>;
