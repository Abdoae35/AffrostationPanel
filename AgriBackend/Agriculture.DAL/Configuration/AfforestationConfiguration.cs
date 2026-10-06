using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agriculture.DAL.Configuration;

public class AfforestationConfiguration :IEntityTypeConfiguration<AfforestationAgricultureAchievement>
{
    public void Configure(EntityTypeBuilder<AfforestationAgricultureAchievement> builder)
    {
       builder.HasOne(a => a.User)
           .WithMany(u => u.AfforestationAchievements)
           .HasForeignKey(a => a.UserId)
           .OnDelete(DeleteBehavior.Restrict);

       builder.HasOne(a=>a.TreeName)
           .WithMany()
           .HasForeignKey(a => a.TreeNameId)
           .OnDelete(DeleteBehavior.Restrict);
       
       builder.HasOne(a=>a.LocationName)
           .WithMany()
           .HasForeignKey(a => a.LocationNameId)
           .OnDelete(DeleteBehavior.Restrict);

    }
}