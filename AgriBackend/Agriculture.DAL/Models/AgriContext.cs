using Agriculture.DAL.Configuration;
using Agriculture.DAL.Repository.LocationNameRepo;

namespace Agriculture.DAL.Models;

public class AgriContext : DbContext
{
    public AgriContext(DbContextOptions<AgriContext> options) : base  (options)
    {
        
    }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TreeTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LocationNamesConfiguration());
        modelBuilder.ApplyConfiguration(new AfforestationConfiguration());
    }


    public DbSet<TreeName> TreeNames { get; set; }
    public DbSet<LocationName> LocationNames { get; set; }
    public DbSet<TreeType> TreeTypes { get; set; }
    public DbSet<AfforestationAgricultureAchievement?> AfforestationAgricultureAchievements { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TypeOfLocation> TypeOfLocations { get; set; }
    
}