using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agriculture.DAL.Configuration;

public class LocationNamesConfiguration :IEntityTypeConfiguration<LocationName>
{
    public void Configure(EntityTypeBuilder<LocationName> builder)
    {
        builder.HasOne(a=>a.LocationType)
            .WithMany(a=>a.LocationNames)
            .HasForeignKey(a=>a.LocationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}