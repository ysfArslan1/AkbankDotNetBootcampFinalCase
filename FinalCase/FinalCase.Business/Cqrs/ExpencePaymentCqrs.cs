using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record CreateExpencePaymentCommand(CreateExpencePaymentRequest Model) : IRequest<ApiResponse<ExpencePaymentResponse>>;
public record UpdateExpencePaymentCommand(int Id, UpdateExpencePaymentRequest Model) : IRequest<ApiResponse>;
public record DeleteExpencePaymentCommand(int Id) : IRequest<ApiResponse>;

public record GetAllExpencePaymentQuery() : IRequest<ApiResponse<List<ExpencePaymentResponse>>>;
public record GetExpencePaymentByIdQuery(int Id) : IRequest<ApiResponse<ExpencePaymentResponse>>;
