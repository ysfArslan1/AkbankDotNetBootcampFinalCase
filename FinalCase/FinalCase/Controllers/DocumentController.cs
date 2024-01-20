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

    // Database bulunan Document verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<DocumentResponse>> Get(int id)
    {
        var operation = new GetDocumentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database de Document verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<DocumentResponse>> Post([FromBody] CreateDocumentRequest Document)
    {
        // Validation iþlemi uygulanýr
        CreateDocumentRequestValidator validator = new CreateDocumentRequestValidator();
        validator.ValidateAndThrow(Document);

        var operation = new CreateDocumentCommand(Document);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateDocumentRequest Document)
    {
        // Validation iþlemi uygulanýr
        UpdateDocumentRequestValidator validator = new UpdateDocumentRequestValidator();
        validator.ValidateAndThrow(Document);

        var operation = new UpdateDocumentCommand(id, Document);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Document verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteDocumentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}