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
public class ExpenceRespondController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpenceRespondController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenceRespondResponse>>> Get()
    {
        var operation = new GetAllExpenceRespondQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceRespond verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenceRespondResponse>> Get(int id)
    {
        var operation = new GetExpenceRespondByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpenceRespond verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenceRespondResponse>> Post([FromBody] CreateExpenceRespondRequest ExpenceRespond)
    {
        // Validation iþlemi uygulanýr
        CreateExpenceRespondRequestValidator validator = new CreateExpenceRespondRequestValidator();
        validator.ValidateAndThrow(ExpenceRespond);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateExpenceRespondCommand(CurrentUserId,ExpenceRespond);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceRespond verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpenceRespondRequest ExpenceRespond)
    {
        // Validation iþlemi uygulanýr
        UpdateExpenceRespondRequestValidator validator = new UpdateExpenceRespondRequestValidator();
        validator.ValidateAndThrow(ExpenceRespond);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateExpenceRespondCommand(id,CurrentUserId, ExpenceRespond);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceRespond verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenceRespondCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<ExpenceRespondResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyExpenceRespondQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceRespond verilerinin çekilmasi için kullanýlýr.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ExpenceRespondResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyExpenceRespondByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }
}