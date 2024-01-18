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

public class RoleQueryHandler :
    IRequestHandler<GetAllRoleQuery, ApiResponse<List<RoleResponse>>>,
    IRequestHandler<GetRoleByIdQuery, ApiResponse<RoleResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public RoleQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Role sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<RoleResponse>>> Handle(GetAllRoleQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Role>().Where(x=> x.IsActive == true)
            .ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<RoleResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<Role>, List<RoleResponse>>(list);
         return new ApiResponse<List<RoleResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen Role deðerlerinin alýndýðý query
    public async Task<ApiResponse<RoleResponse>> Handle(GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Role>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<RoleResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Role, RoleResponse>(entity);
        return new ApiResponse<RoleResponse>(mapped);
    }

}