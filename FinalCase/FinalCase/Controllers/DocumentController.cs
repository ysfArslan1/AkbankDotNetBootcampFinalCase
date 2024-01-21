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
public class DocumentController : ControllerBase
{
    private readonly IMediator mediator;
    public DocumentController(IMediator _mediator)
    {
        mediator = _mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<DocumentResponse>>> Get()
    {
        var operation = new GetAllDocumentQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Document verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<DocumentResponse>> Get(int id)
    {
        var operation = new GetDocumentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Document verisi olu�turmak i�in kullan�l�r.
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<DocumentResponse>> Post([FromBody] CreateDocumentRequest Document)
    {
        // Validation i�lemi uygulan�r
        CreateDocumentRequestValidator validator = new CreateDocumentRequestValidator();
        validator.ValidateAndThrow(Document);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateDocumentCommand(CurrentUserId,Document);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi al�nmak i�in kullan�l�r.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateDocumentRequest Document)
    {
        // Validation i�lemi uygulan�r
        UpdateDocumentRequestValidator validator = new UpdateDocumentRequestValidator();
        validator.ValidateAndThrow(Document);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateDocumentCommand(id,CurrentUserId, Document);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi softdelete yap�l�r
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteDocumentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<DocumentResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyDocumentQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Document verilerinin �ekilmasi i�in kullan�l�r.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<DocumentResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyDocumentByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi al�nmak i�in kullan�l�r.
    [HttpPut("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeePut(int id, [FromBody] UpdateDocumentRequest Document)
    {
        // Validation i�lemi uygulan�r
        UpdateDocumentRequestValidator validator = new UpdateDocumentRequestValidator();
        validator.ValidateAndThrow(Document);

        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateMyDocumentCommand(id, CurrentUserId, Document);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi softdelete yap�l�r
    [HttpDelete("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeeDelete(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new DeleteMyDocumentCommand(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }
}