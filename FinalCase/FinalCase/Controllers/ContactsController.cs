using FinalCase.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vb.Data.DbContext;

namespace FinalCase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IVbDbContext _context;
    public ContactsController(IVbDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Contact>> Get()
    {
        var _list = _context.Contacts.ToList();
        return _list;
    }

    
}