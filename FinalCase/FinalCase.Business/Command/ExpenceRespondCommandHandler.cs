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

    // ExpenceRespond sýnýfýnýn database de oluþturulmasý için kullanýlan command
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

    // ExpenceRespond sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateExpenceRespondCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceRespond>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Explanation = request.Model.Explanation;
        fromdb.isApproved = request.Model.isApproved;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceRespond sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteExpenceRespondCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceRespond>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        // soft delete iþlemi yapýlýr
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}