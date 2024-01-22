using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalCase.Base.Entity;

namespace FinalCase.Data.Entity
{
    public class ExpencePayment:BaseEntity
    {
        public int ExpenceRespondId { get; set; }
        public virtual ExpenceRespond ExpenceRespond { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Description{ get; set; }
        public string TransferType{ get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class ExpencePaymentConfiguration : IEntityTypeConfiguration<ExpencePayment>
    {
        public void Configure(EntityTypeBuilder<ExpencePayment> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.ExpenceRespondId).IsRequired(true);
            builder.Property(x => x.AccountId).IsRequired(true);
            builder.Property(x => x.ReceiverId).IsRequired(true);
            builder.Property(x => x.ReceiverName).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.TransactionDate).IsRequired(true);
            builder.Property(x => x.TransferType).IsRequired(true).HasMaxLength(3);


            builder.HasKey(x => x.Id);
            builder.HasOne(e => e.Account)
            .WithOne()
            .HasForeignKey<ExpencePayment>(e => e.AccountId).IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.ExpenceRespond)
            .WithOne()
            .HasForeignKey<ExpencePayment>(e => e.ExpenceRespondId).IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
