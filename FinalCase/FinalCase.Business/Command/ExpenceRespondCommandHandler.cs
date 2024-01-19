using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class ExpenceRespondCommandHandler :
    IRequestHandler<CreateExpenceRespondCommand, ApiResponse<ExpenceRespondResponse>>,
    IRequestHandler<UpdateExpenceRespondCommand,ApiResponse>,
    IRequestHandler<DeleteExpenceRespondCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceRespondCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceRespond s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<ExpenceRespondResponse>> Handle(CreateExpenceRespondCommand request, CancellationToken cancellationToken)
    {
        var check= await dbContext.Set<ExpenceRespond>().Where(x => x.ExpenceNotifyId == request.Model.ExpenceNotifyId )
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<ExpenceRespondResponse>($"{request.Model.ExpenceNotifyId} is used by another ExpenceRespond.");
        }

        var checkNotify = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Model.ExpenceNotifyId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkNotify == null)
        {
            return new ApiResponse<ExpenceRespondResponse>("ExpenceNotify not found");
        }

        var checkUser = await dbContext.Set<User>().Where(x => x.Id == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkUser == null)
        {
            return new ApiResponse<ExpenceRespondResponse>("User not found");
        }

        var entity = mapper.Map<CreateExpenceRespondRequest, ExpenceRespond>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenceRespond, ExpenceRespondResponse>(entityResult.Entity);
        return new ApiResponse<ExpenceRespondResponse>(mapped);
    }

    // ExpenceRespond s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateExpenceRespondCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceRespond>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Explanation = request.Model.Explanation;
        fromdb.isApproved = request.Model.isApproved;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceRespond s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteExpenceRespondCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceRespond>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete i�lemi yap�l�r
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}