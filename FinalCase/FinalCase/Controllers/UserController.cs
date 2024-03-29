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
public class UserController : ControllerBase
{
    private readonly IMediator mediator;
    public UserController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<UserResponse>>> Get()
    {
        var operation = new GetAllUserQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan User verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<UserResponse>> Get(int id)
    {
        var operation = new GetUserByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de User verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<UserResponse>> Post([FromBody] CreateUserRequest user)
    {
        // Validation i�lemi uygulan�r
        CreateUserRequestValidator validator = new CreateUserRequestValidator();
        validator.ValidateAndThrow(user);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateUserCommand(CurrentUserId, user);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen User verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateUserRequest user)
    {
        // Validation i�lemi uygulan�r
        UpdateUserRequestValidator validator = new UpdateUserRequestValidator();
        validator.ValidateAndThrow(user);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateUserCommand(id,CurrentUserId, user);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen User verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Employee

    // Database bulunan User verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<UserResponse>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetUserByIdQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen User verisi al�nmak i�in kullan�l�r.
    [HttpPut("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeePut( [FromBody] UpdateUserRequest user)
    {
        // Validation i�lemi uygulan�r
        UpdateUserRequestValidator validator = new UpdateUserRequestValidator();
        validator.ValidateAndThrow(user);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateUserCommand(CurrentUserId, CurrentUserId, user);
        var result = await mediator.Send(operation);
        return result;
    }
}