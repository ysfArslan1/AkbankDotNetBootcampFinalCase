using MediatR;
using FinalCase.Base.Response;
using FinalCase.Schema;

namespace FinalCase.Business.Cqrs;


public record EmloyeeReport(int id) : IRequest<ApiResponse<ReportResponse>>;
public record PaymentDensityDay() : IRequest<ApiResponse<ReportResponse>>;
public record PaymentDensityWeek() : IRequest<ApiResponse<ReportResponse>>;
public record PaymentDensityMonth() : IRequest<ApiResponse<ReportResponse>>;
public record GetEmployeeExpenceDensityDay(int id) : IRequest<ApiResponse<ReportResponse>>;
public record GetEmployeeExpenceDensityWeek(int id) : IRequest<ApiResponse<ReportResponse>>;
public record GetEmployeeExpenceDensityMonth(int id) : IRequest<ApiResponse<ReportResponse>>;
public record IsApproveDay() : IRequest<ApiResponse<ReportResponse>>;
public record IsApproveWeek() : IRequest<ApiResponse<ReportResponse>>;
public record IsApproveMonth() : IRequest<ApiResponse<ReportResponse>>;
