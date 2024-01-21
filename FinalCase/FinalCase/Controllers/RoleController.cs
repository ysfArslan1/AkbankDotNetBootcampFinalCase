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
using System.Security.Claims;

namespace FinalCase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator mediator;
    public RoleController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<RoleResponse>>> Get()
    {
        var operation = new GetAllRoleQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Role verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<RoleResponse>> Get(int id)
    {
        var operation = new GetRoleByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Role verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<RoleResponse>> Post([FromBody] CreateRoleRequest Role)
    {
        // Validation iþlemi uygulanýr
        CreateRoleRequestValidator validator = new CreateRoleRequestValidator();
        validator.ValidateAndThrow(Role);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateRoleCommand(CurrentUserId,Role);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Role verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateRoleRequest Role)
    {
        // Validation iþlemi uygulanýr
        UpdateRoleRequestValidator validator = new UpdateRoleRequestValidator();
        validator.ValidateAndThrow(Role);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateRoleCommand(id,CurrentUserId, Role);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Role verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteRoleCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}