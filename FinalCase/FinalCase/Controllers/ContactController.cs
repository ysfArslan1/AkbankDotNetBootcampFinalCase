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
public class ContactController : ControllerBase
{
    private readonly IMediator mediator;
    public ContactController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ContactResponse>>> Get()
    {
        var operation = new GetAllContactQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Contact verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ContactResponse>> Get(int id)
    {
        var operation = new GetContactByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Contact verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ContactResponse>> Post([FromBody] CreateContactRequest Contact)
    {
        // Validation iþlemi uygulanýr
        CreateContactRequestValidator validator = new CreateContactRequestValidator();
        validator.ValidateAndThrow(Contact);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateContactCommand(CurrentUserId,Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateContactRequest Contact)
    {
        // Validation iþlemi uygulanýr
        UpdateContactRequestValidator validator = new UpdateContactRequestValidator();
        validator.ValidateAndThrow(Contact);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateContactCommand(id,CurrentUserId, Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteContactCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<ContactResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyContactQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Contact verilerinin çekilmasi için kullanýlýr.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<ContactResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyContactByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi alýnmak için kullanýlýr.
    [HttpPut("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeePut(int id, [FromBody] UpdateContactRequest Contact)
    {
        // Validation iþlemi uygulanýr
        UpdateContactRequestValidator validator = new UpdateContactRequestValidator();
        validator.ValidateAndThrow(Contact);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateContactCommand(id, CurrentUserId, Contact);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Contact verisi softdelete yapýlýr
    [HttpDelete("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeeDelete(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new DeleteMyContactCommand(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }
}