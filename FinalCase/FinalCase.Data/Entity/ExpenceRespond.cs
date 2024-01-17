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
    public class ExpenceRespond:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ExpenceNotifyId { get; set; }
        public virtual ExpenceNotify ExpenceNotify { get; set; }
        public bool isApproved { get; set; }
        public string Explanation { get; set; }
    }
    public class ExpenceRespondConfiguration : IEntityTypeConfiguration<ExpenceRespond>
    {
        public void Configure(EntityTypeBuilder<ExpenceRespond> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.ExpenceNotifyId).IsRequired(true);
            builder.Property(x => x.Explanation).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.isApproved).IsRequired(true);

        }
    }
}
