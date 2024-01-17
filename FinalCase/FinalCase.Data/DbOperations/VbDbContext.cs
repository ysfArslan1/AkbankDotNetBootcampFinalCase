using Microsoft.EntityFrameworkCore;
using FinalCase.Data.Entity;

namespace FinalCase.Data.DbOperations;

public class VbDbContext : DbContext
{
    public VbDbContext(DbContextOptions<VbDbContext> options) : base(options)
    {

    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ExpenceNotify> ExpenceNotifies { get; set; }
    public DbSet<ExpencePayment> ExpencePayments { get; set; }
    public DbSet<ExpenceRespond> ExpenceResponds { get; set; }
    public DbSet<ExpenceType> ExpenceTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenceNotifyConfiguration());
        modelBuilder.ApplyConfiguration(new ExpencePaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenceRespondConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }

}