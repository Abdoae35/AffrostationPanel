

namespace Agriculture.DAL.Models;

public class EbookContextFactory : IDesignTimeDbContextFactory<AgriContext>
{
    private string cs =
        "Server=localhost; Database=AgriRemoteDB; User Id=sa; Password=Abdo@1234; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; ";
    public AgriContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AgriContext>();
        optionsBuilder.UseSqlServer(cs);

        return new AgriContext(optionsBuilder.Options);
    }
}