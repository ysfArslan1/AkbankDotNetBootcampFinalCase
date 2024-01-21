using AutoMapper;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Query;

public class ReportQueryHandler :
    IRequestHandler<EmloyeeReport, ApiResponse<ReportResponse>>,
    IRequestHandler<PaymentDensityDay, ApiResponse<ReportResponse>>,
    IRequestHandler<PaymentDensityWeek, ApiResponse<ReportResponse>>,
    IRequestHandler<PaymentDensityMonth, ApiResponse<ReportResponse>>,
    IRequestHandler<GetEmployeeExpenceDensityDay, ApiResponse<ReportResponse>>,
    IRequestHandler<GetEmployeeExpenceDensityWeek, ApiResponse<ReportResponse>>,
    IRequestHandler<GetEmployeeExpenceDensityMonth, ApiResponse<ReportResponse>>,
    IRequestHandler<IsApproveDay, ApiResponse<ReportResponse>>,
    IRequestHandler<IsApproveWeek, ApiResponse<ReportResponse>>,
    IRequestHandler<IsApproveMonth, ApiResponse<ReportResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ReportQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ReportResponse>> Handle(EmloyeeReport request, CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.ExpenceNotify.UserId == request.id && x.IsActive == true)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "user name : " + results[0].User.FirstName + " " + results[0].User.LastName + "\n" +
                       "total expence notify : " + results.Count.ToString() + "\n" +
                       "positive expence notify : " + results.Where(x => x.isApproved == true).ToList().Count.ToString() + "\n" +
                       "negative expence notify : " + results.Where(x => x.isApproved == false).ToList().Count.ToString() + "\n" +
                       "requested amount of money : " + results.Sum(x => x.ExpenceNotify.Amount).ToString() + "\n" +
                       "amount of money received : " + results.Where(x => x.isApproved == true).Sum(x => x.ExpenceNotify.Amount).ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(PaymentDensityDay request, CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Day : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-1)).ToList().Count.ToString();

        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }
    public async Task<ApiResponse<ReportResponse>> Handle(PaymentDensityWeek request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Week : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-7)).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }
    public async Task<ApiResponse<ReportResponse>> Handle(PaymentDensityMonth request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Month : " + results.Where(x => x.InsertDate >= DateTime.Now.AddMonths(-1)).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetEmployeeExpenceDensityDay request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true && x.ExpenceNotify.UserId == request.id)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Day : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-1)).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetEmployeeExpenceDensityWeek request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true && x.ExpenceNotify.UserId == request.id)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Week : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-7)).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(GetEmployeeExpenceDensityMonth request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true && x.ExpenceNotify.UserId == request.id)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Payment Density Month : " + results.Where(x => x.InsertDate >= DateTime.Now.AddMonths(-1)).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(IsApproveDay request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true)
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }

        string text = "Aproved Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-1) && x.isApproved == true).ToList().Count.ToString() + "\n" +
            "Rejected Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-1) && x.isApproved == false).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(IsApproveWeek request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true )
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }


        string text = "Aproved Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-7) && x.isApproved == true).ToList().Count.ToString() + "\n" +
            "Rejected Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddDays(-7) && x.isApproved == false).ToList().Count.ToString();
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }

    public async Task<ApiResponse<ReportResponse>> Handle(IsApproveMonth request,
        CancellationToken cancellationToken)
    {
        List<ExpenceRespond> results = await dbContext.Set<ExpenceRespond>()
            .Where(x => x.isApproved == true && x.IsActive == true )
            .Include(x => x.ExpenceNotify)
            .Include(x => x.User)
            .ToListAsync();

        if (results == null)
        {
            return new ApiResponse<ReportResponse>("Records not found");
        }


        string text = "Aproved Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddMonths(-1) && x.isApproved == true).ToList().Count.ToString() + "\n" +
            "Rejected Expence Number : " + results.Where(x => x.InsertDate >= DateTime.Now.AddMonths(-1) && x.isApproved == false).ToList().Count.ToString(); 
        return new ApiResponse<ReportResponse>(new ReportResponse { Report = text });
    }


}