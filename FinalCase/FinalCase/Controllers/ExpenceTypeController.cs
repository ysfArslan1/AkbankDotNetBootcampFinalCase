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
public class ExpenceTypeController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpenceTypeController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    public async Task<ApiResponse<List<ExpenceTypeResponse>>> Get()
    {
        var operation = new GetAllExpenceTypeQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceType verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    public async Task<ApiResponse<ExpenceTypeResponse>> Get(int id)
    {
        var operation = new GetExpenceTypeByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpenceType verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    public async Task<ApiResponse<ExpenceTypeResponse>> Post([FromBody] CreateExpenceTypeRequest ExpenceType)
    {
        // Validation i�lemi uygulan�r
        CreateExpenceTypeRequestValidator validator = new CreateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        var operation = new CreateExpenceTypeCommand(ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpenceTypeRequest ExpenceType)
    {
        // Validation i�lemi uygulan�r
        UpdateExpenceTypeRequestValidator validator = new UpdateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        var operation = new UpdateExpenceTypeCommand(id, ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenceTypeCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}