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

    // Database bulunan ExpenceType verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    public async Task<ApiResponse<ExpenceTypeResponse>> Get(int id)
    {
        var operation = new GetExpenceTypeByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpenceType verisi oluþturmak için kullanýlýr.
    [HttpPost]
    public async Task<ApiResponse<ExpenceTypeResponse>> Post([FromBody] CreateExpenceTypeRequest ExpenceType)
    {
        // Validation iþlemi uygulanýr
        CreateExpenceTypeRequestValidator validator = new CreateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        var operation = new CreateExpenceTypeCommand(ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpenceTypeRequest ExpenceType)
    {
        // Validation iþlemi uygulanýr
        UpdateExpenceTypeRequestValidator validator = new UpdateExpenceTypeRequestValidator();
        validator.ValidateAndThrow(ExpenceType);

        var operation = new UpdateExpenceTypeCommand(id, ExpenceType);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceType verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenceTypeCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}