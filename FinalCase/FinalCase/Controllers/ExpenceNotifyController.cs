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
public class ExpenceNotifyController : ControllerBase
{
    private readonly IMediator mediator;
    public ExpenceNotifyController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenceNotifyResponse>>> Get()
    {
        var operation = new GetAllExpenceNotifyQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceNotify verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ExpenceNotifyResponse>> Get(int id)
    {
        var operation = new GetExpenceNotifyByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceNotify verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("FromExpenceType/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ExpenceNotifyResponse>>> GetFromExpenceType(int id)
    {
        var operation = new GetAllExpenceNotifyFromExpenceTypeQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de ExpenceNotify verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ExpenceNotifyResponse>> Post([FromBody] CreateExpenceNotifyRequest ExpenceNotify)
    {
        // Validation i�lemi uygulan�r
        CreateExpenceNotifyRequestValidator validator = new CreateExpenceNotifyRequestValidator();
        validator.ValidateAndThrow(ExpenceNotify);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateExpenceNotifyCommand(CurrentUserId,ExpenceNotify);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceNotify verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateExpenceNotifyRequest ExpenceNotify)
    {
        // Validation i�lemi uygulan�r
        UpdateExpenceNotifyRequestValidator validator = new UpdateExpenceNotifyRequestValidator();
        validator.ValidateAndThrow(ExpenceNotify);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateExpenceNotifyCommand(id,CurrentUserId, ExpenceNotify);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceNotify verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteExpenceNotifyCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    //Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<ExpenceNotifyResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyExpenceNotifyQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan ExpenceNotify verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ExpenceNotifyResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyExpenceNotifyByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceNotify verisi al�nmak i�in kullan�l�r.
    [HttpPut("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeePut(int id, [FromBody] UpdateExpenceNotifyRequest ExpenceNotify)
    {
        // Validation i�lemi uygulan�r
        UpdateExpenceNotifyRequestValidator validator = new UpdateExpenceNotifyRequestValidator();
        validator.ValidateAndThrow(ExpenceNotify);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateMyExpenceNotifyCommand(id, CurrentUserId, ExpenceNotify);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen ExpenceNotify verisi softdelete yap�l�r
    [HttpDelete("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeeDelete(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new DeleteMyExpenceNotifyCommand(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }
}