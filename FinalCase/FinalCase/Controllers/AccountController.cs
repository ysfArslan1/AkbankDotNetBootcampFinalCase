using FinalCase.Base.Response;
using FinalCase.Business.Cqrs;
using FinalCase.Business.Validator;
using FinalCase.Data;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace FinalCase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IDistributedCache distributedCache;
    public AccountController(IMediator _mediator, IDistributedCache cache)
    {
        mediator = _mediator;
        distributedCache = cache;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<AccountResponse>>> Get()
    {
        var operation = new GetAllAccountQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Account verilerinin çekilmasi için kullanýlýr.
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AccountResponse>> Get(int id)
    {

        var operation = new GetAccountByIdQuery(id);
        var result = await mediator.Send(operation);

        return result;
    }

    // Database bulunan Account verilerinin çekilmasi için kullanýlýr.
    [HttpGet("GetByAcoountNumber/{AccountNumber}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ApiResponse<AccountResponse>> GetByAcoountNumber(int AccountNumber)
    {
        var cacheResult = await distributedCache.GetAsync(AccountNumber.ToString());
        if (cacheResult != null)
        {
            string json = Encoding.UTF8.GetString(cacheResult);
            var response = JsonConvert.DeserializeObject<AccountResponse>(json);
            return new ApiResponse<AccountResponse>(response);
        }

        var operation = new GetAccountByAccountNumberQuery(AccountNumber);
        var result = await mediator.Send(operation);

        if (result.Response != null)
        {
            string responseJson = JsonConvert.SerializeObject(result.Response);
            byte[] responseArr = Encoding.UTF8.GetBytes(responseJson);

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            await distributedCache.SetAsync(AccountNumber.ToString(), responseArr, options);
        }
        return result;
    }

    [HttpDelete("DeleteCache/{AccountNumber}")]
    public async Task<ApiResponse> DeleteCache(int AccountNumber)
    {
        distributedCache.Remove(AccountNumber.ToString());
        return new ApiResponse();
    }

    // Database de Account verisi oluþturmak için kullanýlýr.
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<AccountResponse>> Post([FromBody] CreateAccountRequest Account)
    {
        // Validation iþlemi uygulanýr
        CreateAccountRequestValidator validator = new CreateAccountRequestValidator();
        validator.ValidateAndThrow(Account);


        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new CreateAccountCommand(Account, CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Account verisi alýnmak için kullanýlýr.
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateAccountRequest Account)
    {
        // Validation iþlemi uygulanýr
        UpdateAccountRequestValidator validator = new UpdateAccountRequestValidator();
        validator.ValidateAndThrow(Account);


        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateAccountCommand(id,CurrentUserId, Account);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Account verisi softdelete yapýlýr
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAccountCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }

    // Employee

    [HttpGet("Employee")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<List<AccountResponse>>> EmployeeGet()
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetAllMyAccountQuery(CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database bulunan Account verilerinin çekilmasi için kullanýlýr.
    [HttpGet("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse<AccountResponse>> EmployeeGet(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new GetMyAccountByIdQuery(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }


    // Database den id degeri verilen Account verisi alýnmak için kullanýlýr.
    [HttpPut("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeePut(int id, [FromBody] UpdateAccountRequest Account)
    {
        // Validation iþlemi uygulanýr
        UpdateAccountRequestValidator validator = new UpdateAccountRequestValidator();
        validator.ValidateAndThrow(Account);


        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new UpdateAccountCommand(id, CurrentUserId, Account);
        var result = await mediator.Send(operation);
        return result;
    }

    // Database den id degeri verilen Account verisi softdelete yapýlýr
    [HttpDelete("Employee/{id}")]
    [Authorize(Roles = "Admin,Employee")]
    public async Task<ApiResponse> EmployeeDelete(int id)
    {
        string _id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;
        int CurrentUserId = int.Parse(_id);

        var operation = new DeleteMyAccountCommand(id,CurrentUserId);
        var result = await mediator.Send(operation);
        return result;
    }
}