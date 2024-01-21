using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceNotifyCommand(int CurrentUserId, CreateExpenceNotifyRequest Model) : IRequest<ApiResponse<ExpenceNotifyResponse>>;
public record UpdateExpenceNotifyCommand(int Id, int CurrentUserId, UpdateExpenceNotifyRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceNotifyCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceNotifyQuery() : IRequest<ApiResponse<List<ExpenceNotifyResponse>>>;
public record GetExpenceNotifyByIdQuery(int Id) : IRequest<ApiResponse<ExpenceNotifyResponse>>;

// Employee
public record UpdateMyExpenceNotifyCommand(int Id, int CurrentUserId, UpdateExpenceNotifyRequest Model) : IRequest<ApiResponse>;
public record DeleteMyExpenceNotifyCommand(int Id, int CurrentUserId) : IRequest<ApiResponse>;

public record GetAllMyExpenceNotifyQuery(int CurrentUserId) : IRequest<ApiResponse<List<ExpenceNotifyResponse>>>;
public record GetMyExpenceNotifyByIdQuery(int Id, int CurrentUserId) : IRequest<ApiResponse<ExpenceNotifyResponse>>;
