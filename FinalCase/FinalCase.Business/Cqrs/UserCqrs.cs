using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateUserCommand(int CurrentUserId, CreateUserRequest Model) : IRequest<ApiResponse<UserResponse>>;
public record UpdateUserCommand(int Id, int CurrentUserId, UpdateUserRequest Model) : IRequest<ApiResponse>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;

