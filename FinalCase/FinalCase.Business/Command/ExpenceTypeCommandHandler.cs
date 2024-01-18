using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class ExpenceTypeCommandHandler :
    IRequestHandler<CreateExpenceTypeCommand, ApiResponse<ExpenceTypeResponse>>,
    IRequestHandler<UpdateExpenceTypeCommand,ApiResponse>,
    IRequestHandler<DeleteExpenceTypeCommand,ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenceTypeCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // ExpenceType sýnýfýnýn database de oluþturulmasý için kullanýlan command
    public async Task<ApiResponse<ExpenceTypeResponse>> Handle(CreateExpenceTypeCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<ExpenceType>().Where(x => x.Name == request.Model.Name )
            .FirstOrDefaultAsync(cancellationToken);
        if (check != null)
        {
            return new ApiResponse<ExpenceTypeResponse>($"{request.Model.Name} is used by another ExpenceType.");
        }
        var entity = mapper.Map<ExpenceTypeRequest, ExpenceType>(request.Model);
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<ExpenceType, ExpenceTypeResponse>(entityResult.Entity);
        return new ApiResponse<ExpenceTypeResponse>(mapped);
    }

    // ExpenceType sýnýfýnýn database de güncellenmesi için kullanýlan command
    public async Task<ApiResponse> Handle(UpdateExpenceTypeCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceType>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // deðerin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.Name = request.Model.Name;
        fromdb.Description = request.Model.Description;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // ExpenceType sýnýfýnýn database de softdelete ile silinmesini için kullanýlan command
    public async Task<ApiResponse> Handle(DeleteExpenceTypeCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<ExpenceType>().Where(x => x.Id == request.Id)
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