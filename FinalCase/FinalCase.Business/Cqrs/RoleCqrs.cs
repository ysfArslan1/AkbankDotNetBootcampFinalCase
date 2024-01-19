using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateRoleCommand(CreateRoleRequest Model) : IRequest<ApiResponse<RoleResponse>>;
public record UpdateRoleCommand(int Id,UpdateRoleRequest Model) : IRequest<ApiResponse>;
public record DeleteRoleCommand(int Id) : IRequest<ApiResponse>;

public record GetAllRoleQuery() : IRequest<ApiResponse<List<RoleResponse>>>;
public record GetRoleByIdQuery(int Id) : IRequest<ApiResponse<RoleResponse>>;
