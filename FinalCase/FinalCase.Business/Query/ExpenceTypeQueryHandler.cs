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

public class ExpenceTypeQueryHandler :
    IRequestHandler<GetAllExpenceTypeQuery, ApiResponse<List<ExpenceTypeResponse>>>,
    IRequestHandler<GetExpenceTypeByIdQuery, ApiResponse<ExpenceTypeResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceTypeQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceType sýnýfýnýn database içerisinde bulunan verilerinin alýndýgý query
    public async Task<ApiResponse<List<ExpenceTypeResponse>>> Handle(GetAllExpenceTypeQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<ExpenceType>().Where(x=> x.IsActive == true)
            .ToListAsync(cancellationToken);

        // deðerin kontrol edilmesi
        if (list == null)
        {
            return new ApiResponse<List<ExpenceTypeResponse>>("Record not found");
        }

        var mappedList = mapper.Map<List<ExpenceType>, List<ExpenceTypeResponse>>(list);
         return new ApiResponse<List<ExpenceTypeResponse>>(mappedList);
    }

    // Ýd deðeri ile istenilen ExpenceType deðerlerinin alýndýðý query
    public async Task<ApiResponse<ExpenceTypeResponse>> Handle(GetExpenceTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<ExpenceType>()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsActive==true, cancellationToken);

        // deðerin kontrol edilmesi
        if (entity == null)
        {
            return new ApiResponse<ExpenceTypeResponse>("Record not found");
        }
        
        var mapped = mapper.Map<ExpenceType, ExpenceTypeResponse>(entity);
        return new ApiResponse<ExpenceTypeResponse>(mapped);
    }

}