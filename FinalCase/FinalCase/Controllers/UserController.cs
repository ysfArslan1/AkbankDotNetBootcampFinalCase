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
public class UserController : ControllerBase
{
    private readonly IMediator mediator;
    public UserController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<UserResponse>>> Get()
    {
        var operation = new GetAllUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan User verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<UserResponse>> Get(int id)
    {
        var operation = new GetUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de User verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] CreateUserRequest User)
    {
        // Validation iþlemi uygulanýr
        CreateUserRequestValidator validator = new CreateUserRequestValidator();
        validator.ValidateAndThrow(User);

        var operation = new CreateUserCommand(User);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen User verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateUserRequest User)
    {
        // Validation iþlemi uygulanýr
        UpdateUserRequestValidator validator = new UpdateUserRequestValidator();
        validator.ValidateAndThrow(User);

        var operation = new UpdateUserCommand(id, User);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen User verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}