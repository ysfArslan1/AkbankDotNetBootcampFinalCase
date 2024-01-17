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
    public class ExpenceNotify:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Explanation { get; set; }
        public decimal Amount{ get; set; }
        public string TransferType { get; set; }

        public int ExpenceTypeId { get; set; }
        public virtual ExpenceType ExpenceType { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
    public class ExpenceNotifyConfiguration : IEntityTypeConfiguration<ExpenceNotify>
    {
        public void Configure(EntityTypeBuilder<ExpenceNotify> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Explanation).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Amount).IsRequired(true);
            builder.Property(x => x.TransferType).IsRequired(true).HasMaxLength(3);
            builder.Property(x => x.ExpenceTypeId).IsRequired(true);


            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Documents)
                .WithOne(x => x.ExpenceNotify)
                .HasForeignKey(x => x.Id)
                .IsRequired(true)
            .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<ExpenceNotify>(e => e.UserId).IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.ExpenceType)
            .WithOne()
            .HasForeignKey<ExpenceNotify>(e => e.ExpenceTypeId).IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
