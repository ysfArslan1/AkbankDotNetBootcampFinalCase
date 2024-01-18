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

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UserQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // User sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<User>().Where(x=> x.IsActive == true)
            .Include(x => x.Role).ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<UserResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<User>, List<UserResponse>>(list);
         return new ApiResponse<List<UserResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen User deðerlerinin alýndýðý query
    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<User>()
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<UserResponse>("Record not found");
        }
        
        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

}