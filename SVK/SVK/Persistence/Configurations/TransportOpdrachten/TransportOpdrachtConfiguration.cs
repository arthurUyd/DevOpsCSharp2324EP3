using SVK.Domain.TransportOpdrachten;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SVK.Persistence.Configuration;


namespace SVK.Persistence.Configurations.TransportOpdrachten;

internal class TransportOpdrachtConfiguration : EntityConfiguration<TransportOpdracht>
{
    public override void Configure(EntityTypeBuilder<TransportOpdracht> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.Lader);
        builder.HasMany(x => x.Laadbonnen);
    }
}
