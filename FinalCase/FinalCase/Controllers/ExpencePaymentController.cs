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

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateExpencePaymentCommand(CurrentUserId,ExpencePayment);
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

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateExpencePaymentCommand(id,CurrentUserId, ExpencePayment);
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

    // Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<ExpencePaymentResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyExpencePaymentQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpencePayment verilerinin çekilmasi için kullanýlýr.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ExpencePaymentResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyExpencePaymentByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

}