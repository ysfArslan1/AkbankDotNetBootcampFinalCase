using FinalCase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vb.Data.DbContext
{
    public interface IVbDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<ExpenceNotify> ExpenceNotifies { get; set; }
        public DbSet<ExpencePayment> expencePayments { get; set; }
        public DbSet<ExpenceRespond> expenceResponds { get; set; }
        public DbSet<ExpenceType> ExpenceTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
