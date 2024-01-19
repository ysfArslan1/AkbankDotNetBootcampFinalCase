using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpenceTypeCommand(CreateExpenceTypeRequest Model) : IRequest<ApiResponse<ExpenceTypeResponse>>;
public record UpdateExpenceTypeCommand(int Id,UpdateExpenceTypeRequest Model) : IRequest<ApiResponse>;
public record DeleteExpenceTypeCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpenceTypeQuery() : IRequest<ApiResponse<List<ExpenceTypeResponse>>>;
public record GetExpenceTypeByIdQuery(int Id) : IRequest<ApiResponse<ExpenceTypeResponse>>;
