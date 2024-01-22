using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Schema;
using FinalCase.Services;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IMediator mediator;

    public ReportsController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("EmloyeeReport")]
    public async Task<ApiResponse<ReportResponse>> EmloyeeReport()
    {
        var operation = new EmloyeeReport(2);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("PaymentDensityDay")]
    public async Task<ApiResponse<ReportResponse>> PaymentDensityDay()
    {
        var operation = new PaymentDensityDay();
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("PaymentDensityWeek")]
    public async Task<ApiResponse<ReportResponse>> PaymentDensityWeek()
    {
        var operation = new PaymentDensityWeek();
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("PaymentDensityMonth")]
    public async Task<ApiResponse<ReportResponse>> PaymentDensityMonth()
    {
        var operation = new PaymentDensityMonth();
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("GetEmployeeExpenceDensityDay")]
    public async Task<ApiResponse<ReportResponse>> GetEmployeeExpenceDensityDay()
    {
        var operation = new GetEmployeeExpenceDensityDay(1);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("GetEmployeeExpenceDensityWeek")]
    public async Task<ApiResponse<ReportResponse>> GetEmployeeExpenceDensityWeek()
    {
        var operation = new GetEmployeeExpenceDensityWeek(1);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("GetEmployeeExpenceDensityMonth")]
    public async Task<ApiResponse<ReportResponse>> GetEmployeeExpenceDensityMonth()
    {
        var operation = new GetEmployeeExpenceDensityMonth(1);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("IsApproveDay")]
    public async Task<ApiResponse<ReportResponse>> IsApproveDay()
    {
        var operation = new IsApproveDay();
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("IsApproveWeek")]
    public async Task<ApiResponse<ReportResponse>> IsApproveWeek()
    {
        var operation = new IsApproveWeek();
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("IsApproveMonth")]
    public async Task<ApiResponse<ReportResponse>> IsApproveMonth()
    {
        var operation = new IsApproveMonth();
        var result = await mediator.Send(operation);
        return result;
    }

}