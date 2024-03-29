﻿using FinalCase.Base.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class Contact:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.InsertUserId).IsRequired(true);
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(11);

            builder.HasKey(x => x.Id);
            builder.HasOne(e => e.User)
            .WithOne()
            .HasForeignKey<Contact>(e => e.UserId).IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
