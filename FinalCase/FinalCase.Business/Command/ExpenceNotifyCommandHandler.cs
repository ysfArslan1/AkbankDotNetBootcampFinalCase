using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class ExpenceNotifyCommandHandler :
    IRequestHandler<CreateExpenceNotifyCommand, ApiResponse<ExpenceNotifyResponse>>,
    IRequestHandler<UpdateExpenceNotifyCommand,ApiResponse>,
    IRequestHandler<DeleteExpenceNotifyCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceNotifyCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceNotify s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<ExpenceNotifyResponse>> Handle(CreateExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        

        var entity = mapper.Map<CreateExpenceNotifyRequest, ExpenceNotify>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenceNotify, ExpenceNotifyResponse>(entityResult.Entity);
        return new ApiResponse<ExpenceNotifyResponse>(mapped);
    }

    // ExpenceNotify s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.ExpenceTypeId = request.Model.ExpenceTypeId;
        fromdb.Explanation = request.Model.Explanation;
        fromdb.Amount = request.Model.Amount;
        fromdb.TransferType = request.Model.TransferType;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceNotify s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteExpenceNotifyCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceNotify>().Where(x => x.Id == request.Id)
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