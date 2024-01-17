using FinalCase.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class Account:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string Name { get; set; }
    }

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.AccountNumber).ValueGeneratedNever();

            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.AccountNumber).IsRequired(true);
            builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);
            builder.Property(x => x.Balance).IsRequired(true).HasPrecision(18, 4);
            builder.Property(x => x.CurrencyType).IsRequired(true).HasMaxLength(3);
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(100);

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.AccountNumber).IsUnique(true);
            builder.HasKey(x => x.AccountNumber);

            builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Account>(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
