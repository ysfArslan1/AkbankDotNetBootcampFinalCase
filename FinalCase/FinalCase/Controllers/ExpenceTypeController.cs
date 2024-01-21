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
public class ExpenceTypeController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpenceTypeController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<ExpenceTypeResponse>>> Get()
    {
        var operation = new GetAllExpenceTypeQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceType verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ExpenceTypeResponse>> Get(int id)
    {
        var operation = new GetExpenceTypeByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpenceType verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenceTypeResponse>> Post([FromBody] CreateExpenceTypeRequest ExpenceType)
    {
        // Validation iþlemi uygulanýr
        CreateExpenceTypeRequestValidator validator = new CreateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateExpenceTypeCommand(CurrentUserId,ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpenceTypeRequest ExpenceType)
    {
        // Validation iþlemi uygulanýr
        UpdateExpenceTypeRequestValidator validator = new UpdateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateExpenceTypeCommand(id,CurrentUserId, ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenceTypeCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}