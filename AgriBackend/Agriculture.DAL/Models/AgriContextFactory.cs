namespace Agriculture.DAL.Models;

public class EbookContextFactory : IDesignTimeDbContextFactory<AgriContext>
{
    public AgriContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__cs")
            ?? throw new InvalidOperationException("Set ConnectionStrings__cs for EF design-time commands.");
        var optionsBuilder = new DbContextOptionsBuilder<AgriContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new AgriContext(optionsBuilder.Options);
    }
}
