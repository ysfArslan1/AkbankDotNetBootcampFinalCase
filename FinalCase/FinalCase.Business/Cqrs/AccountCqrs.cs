using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateAccountCommand(AccountRequest Model) : IRequest<ApiResponse<AccountResponse>>;
public record UpdateAccountCommand(int Id,AccountRequest Model) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAccountQuery() : IRequest<ApiResponse<List<AccountResponse>>>;
public record GetAccountByIdQuery(int Id) : IRequest<ApiResponse<AccountResponse>>;
