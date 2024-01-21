using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateContactCommand(int CurrentUserId, CreateContactRequest Model) : IRequest<ApiResponse<ContactResponse>>;
public record UpdateContactCommand(int Id, int CurrentUserId, UpdateContactRequest Model) : IRequest<ApiResponse>;
public record DeleteContactCommand(int Id) : IRequest<ApiResponse>;

public record GetAllContactQuery() : IRequest<ApiResponse<List<ContactResponse>>>;
public record GetContactByIdQuery(int Id) : IRequest<ApiResponse<ContactResponse>>;


// Employee
public record UpdateMyContactCommand(int Id, int CurrentUserId, UpdateContactRequest Model) : IRequest<ApiResponse>;
public record DeleteMyContactCommand(int Id, int CurrentUserId) : IRequest<ApiResponse>;

public record GetAllMyContactQuery(int CurrentUserId) : IRequest<ApiResponse<List<ContactResponse>>>;
public record GetMyContactByIdQuery(int Id, int CurrentUserId) : IRequest<ApiResponse<ContactResponse>>;
