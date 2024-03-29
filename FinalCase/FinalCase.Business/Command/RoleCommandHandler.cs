using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class RoleCommandHandler :
    IRequestHandler<CreateRoleCommand, ApiResponse<RoleResponse>>,
    IRequestHandler<UpdateRoleCommand,ApiResponse>,
    IRequestHandler<DeleteRoleCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public RoleCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Role s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<RoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<Role>().Where(x => x.Name == request.Model.Name )
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<RoleResponse>($"{request.Model.Name} is used by another Role.");
        }

        var entity = mapper.Map<CreateRoleRequest, Role>(request.Model);
        entity.InsertUserId = request.CurrentUserId;
        entity.InsertDate = DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Role, RoleResponse>(entityResult.Entity);
        return new ApiResponse<RoleResponse>(mapped);
    }

    // Role s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Role>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var check = await dbContext.Set<Role>().Where(x => x.Name == request.Model.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse($"{request.Model.Name} is used by another Role.");
        }

        fromdb.Name = request.Model.Name;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Role s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Role>().Where(x => x.Id == request.Id)
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