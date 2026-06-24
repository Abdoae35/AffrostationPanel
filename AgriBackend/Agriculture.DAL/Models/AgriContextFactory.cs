

namespace Agriculture.DAL.Models;

public class EbookContextFactory : IDesignTimeDbContextFactory<AgriContext>
{
    private string cs =
        "Server=db49161.public.databaseasp.net; Database=db49161; User Id=db49161; Password=nX-6=x9ZD8#y; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";
    public AgriContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AgriContext>();
        optionsBuilder.UseSqlServer(cs);

        return new AgriContext(optionsBuilder.Options);
    }
}