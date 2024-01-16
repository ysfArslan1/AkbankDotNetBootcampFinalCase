using Microsoft.EntityFrameworkCore;
using FinalCase.Data.Entity;
using Vb.Data.DbContext;

namespace FinalCase.Data;

public class VbDbContext : DbContext, IVbDbContext
{
    public VbDbContext(DbContextOptions<VbDbContext> options): base(options)
    {
    
    }   
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ExpenceNotify> ExpenceNotifies { get; set; }
    public DbSet<ExpencePayment> expencePayments { get; set; }
    public DbSet<ExpenceRespond> expenceResponds { get; set; }
    public DbSet<ExpenceType> ExpenceTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}