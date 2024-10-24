﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Persistence.Configuration;

class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // All tables are singlular named and have the name of the Entity
        builder.ToTable(typeof(T).Name);
        // All entities can be soft deleted but are not by default
        builder.Property(x => x.IsEnabled).IsRequired().HasDefaultValue(true).ValueGeneratedNever();
        // Default SQL constraint for CreatedAt and UpdatedAt
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()").IsConcurrencyToken();
    }
}
