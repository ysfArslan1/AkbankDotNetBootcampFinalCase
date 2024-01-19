using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<UpdateUserCommand,ApiResponse>,
    IRequestHandler<DeleteUserCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UserCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // User s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<User>().Where(x => x.IdentityNumber == request.Model.IdentityNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<UserResponse>($"{request.Model.IdentityNumber} is used by another User.");
        }
        var checkRole = await dbContext.Set<Role>().Where(x => x.Id == request.Model.RoleId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkRole == null)
        {
            return new ApiResponse<UserResponse>("Role not found");
        }


        var entity = mapper.Map<CreateUserRequest, User>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<User, UserResponse>(entityResult.Entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    // User s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var checkRole = await dbContext.Set<Role>().Where(x => x.Id == request.Model.RoleId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkRole == null)
        {
            return new ApiResponse("Role not found");
        }

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.DateOfBirth = request.Model.DateOfBirtht;
        fromdb.RoleId = request.Model.RoleId;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // User s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<User>().Where(x => x.Id == request.Id)
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