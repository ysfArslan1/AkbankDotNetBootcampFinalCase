using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;
using Azure.Core;

namespace FinalCase.Business.Command;

public class ContactCommandHandler :
    IRequestHandler<CreateContactCommand, ApiResponse<ContactResponse>>,
    IRequestHandler<UpdateContactCommand,ApiResponse>,
    IRequestHandler<DeleteContactCommand,ApiResponse>,
    IRequestHandler<UpdateMyContactCommand, ApiResponse>,
    IRequestHandler<DeleteMyContactCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public ContactCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Contact s�n�f�n�n database de olu�turulmas� i�in kullan�lan command
    public async Task<ApiResponse<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var check = await dbContext.Set<User>().Where(x => x.Id == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (check == null)
        {
            return new ApiResponse<ContactResponse>("User not found");
        }
        var checkEmail= await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email )
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmail != null)
        {
            return new ApiResponse<ContactResponse>($"{request.Model.Email} is used by another Contact.");
        }
        var checkPhone = await dbContext.Set<Contact>().Where(x => x.PhoneNumber == request.Model.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkPhone != null)
        {
            return new ApiResponse<ContactResponse>($"{request.Model.PhoneNumber} is used by another Contact.");
        }

        var entity = mapper.Map<CreateContactRequest, Contact>(request.Model);
        entity.InsertUserId=request.CurrentUserId;
        entity.InsertDate=DateTime.Now;

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Contact, ContactResponse>(entityResult.Entity);
        return new ApiResponse<ContactResponse>(mapped);
    }

    // Contact s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var checkEmail = await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmail != null)
        {
            return new ApiResponse($"{request.Model.Email} is used by another Contact.");
        }
        var checkPhone = await dbContext.Set<Contact>().Where(x => x.PhoneNumber == request.Model.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkPhone != null)
        {
            return new ApiResponse($"{request.Model.PhoneNumber} is used by another Contact.");
        }

        fromdb.Email = request.Model.Email;
        fromdb.PhoneNumber = request.Model.PhoneNumber;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Contact s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id)
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

    // Employee

    // Contact s�n�f�n�n database de g�ncellenmesi i�in kullan�lan command
    public async Task<ApiResponse> Handle(UpdateMyContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);
        // de�erin kontrol edilmesi
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        var checkEmail = await dbContext.Set<Contact>().Where(x => x.Email == request.Model.Email)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmail != null)
        {
            return new ApiResponse($"{request.Model.Email} is used by another Contact.");
        }
        var checkPhone = await dbContext.Set<Contact>().Where(x => x.PhoneNumber == request.Model.PhoneNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkPhone != null)
        {
            return new ApiResponse($"{request.Model.PhoneNumber} is used by another Contact.");
        }

        fromdb.Email = request.Model.Email;
        fromdb.PhoneNumber = request.Model.PhoneNumber;
        fromdb.UpdateUserId = request.CurrentUserId;
        fromdb.UpdateDate = DateTime.Now;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    // Contact s�n�f�n�n database de softdelete ile silinmesini i�in kullan�lan command
    public async Task<ApiResponse> Handle(DeleteMyContactCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Contact>().Where(x => x.Id == request.Id && x.UserId == request.CurrentUserId)
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