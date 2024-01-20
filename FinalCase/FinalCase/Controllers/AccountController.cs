using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Business.Validator;
using FinalCase.Data;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator mediator;
    public AccountController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AccountResponse>>> Get()
    {
        var operation = new GetAllAccountQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Account verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AccountResponse>> Get(int id)
    {
        var operation = new GetAccountByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Account verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] CreateAccountRequest Account)
    {
        // Validation i�lemi uygulan�r
        CreateAccountRequestValidator validator = new CreateAccountRequestValidator();
        validator.ValidateAndThrow(Account);

        var operation = new CreateAccountCommand(Account);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Account verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateAccountRequest Account)
    {
        // Validation i�lemi uygulan�r
        UpdateAccountRequestValidator validator = new UpdateAccountRequestValidator();
        validator.ValidateAndThrow(Account);

        var operation = new UpdateAccountCommand(id, Account);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Account verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAccountCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}