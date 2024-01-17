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
    public class ExpenceType:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ExpenceTypeConfiguration : IEntityTypeConfiguration<ExpenceType>
    {
        public void Configure(EntityTypeBuilder<ExpenceType> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(200);

            builder.HasKey(x => x.Id);
        }
    }
}
