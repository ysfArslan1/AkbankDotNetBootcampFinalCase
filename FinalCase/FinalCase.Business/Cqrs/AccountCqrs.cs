using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateAccountCommand(CreateAccountRequest Model, int CurrentUserId) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(int Id, int CurrentUserId, UpdateAccountRequest Model) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;

// Employee
public record UpdateMyAccountCommand(int Id, int CurrentUserId, UpdateAccountRequest Model) : IRequest<ApiResponse>;
public record DeleteMyAccountCommand(int Id, int CurrentUserId) : IRequest<ApiResponse>;

public record GetAllMyAccountQuery( int CurrentUserId) : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetMyAccountByIdQuery(int Id, int CurrentUserId) : IRequest<ApiResponse<AccountResponse>>;
