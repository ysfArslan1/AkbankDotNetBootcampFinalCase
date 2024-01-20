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
public class ExpencePaymentController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpencePaymentController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpencePaymentResponse>>> Get()
    {
        var operation = new GetAllExpencePaymentQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpencePayment verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpencePaymentResponse>> Get(int id)
    {
        var operation = new GetExpencePaymentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpencePayment verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpencePaymentResponse>> Post([FromBody] CreateExpencePaymentRequest ExpencePayment)
    {
        // Validation iþlemi uygulanýr
        CreateExpencePaymentRequestValidator validator = new CreateExpencePaymentRequestValidator();
        validator.ValidateAndThrow(ExpencePayment);

        var operation = new CreateExpencePaymentCommand(ExpencePayment);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpencePayment verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpencePaymentRequest ExpencePayment)
    {
        // Validation iþlemi uygulanýr
        UpdateExpencePaymentRequestValidator validator = new UpdateExpencePaymentRequestValidator();
        validator.ValidateAndThrow(ExpencePayment);

        var operation = new UpdateExpencePaymentCommand(id, ExpencePayment);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpencePayment verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpencePaymentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}