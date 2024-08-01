using Domain.TransportOpdracht;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration;

internal class TransportOpdrachtConfiguration : EntityConfiguration<TransportOpdracht>
{
    public override void Configure(EntityTypeBuilder<TransportOpdracht> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(x => x.Lader).Property(x => x.Naam);
        builder.OwnsMany(x => x.Producten).Property(x => x.Id);
    }
}
