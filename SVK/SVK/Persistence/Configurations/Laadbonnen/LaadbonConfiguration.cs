using SVK.Domain.Laadbonnen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SVK.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVK.Persistence.Configurations.Laadbonnen;

internal class LaadbonConfiguration: EntityConfiguration<Laadbon>
{
    public override void Configure(EntityTypeBuilder<Laadbon> builder)
    {
        base.Configure(builder);
        builder.Property(l => l.Bestandurl)
            .HasMaxLength(255) 
            .IsRequired(); 
        builder.OwnsOne(x => x.Address, address =>
        {
            // Without this mapping EF Core does not save the properties since they're getters only.
            // This can be omitted by making them private set, but then you're lying to the domain model.
            address.Property(x => x.Addressline1).HasColumnName(nameof(Address.Addressline1));
            address.Property(x => x.Addressline2).HasColumnName(nameof(Address.Addressline2));
            address.Property(x => x.PostalCode).HasColumnName(nameof(Address.PostalCode));
            address.Property(x => x.City).HasColumnName(nameof(Address.City));
            address.Property(x => x.Country).HasColumnName(nameof(Address.Country));
        });


    }
}
