namespace Agriculture.DAL.Repository.TreeTypeRepo;

public class TreeTypeRepo :ITreeTypeRepo
{
    private readonly AgriContext _context;

    public TreeTypeRepo(AgriContext context)
    {
        _context = context;
    }

    public  IQueryable<TreeType> GetAll()
    {
        return _context.TreeTypes
            .OrderBy(t=>t.Type)
            .AsNoTracking();
    }

    public async Task<TreeType?> GetById(int id)
    {
        return await _context.TreeTypes
            .FindAsync(id);
    }

    public async Task InsertAsync(TreeType tree)
    {
        _context.TreeTypes.Add(tree);
       await _context.SaveChangesAsync();
        
    }

    public async Task Update(TreeType tree)
    {
       _context.TreeTypes.Update(tree);
       await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TreeType tree)
    {
       _context.TreeTypes.Remove(tree);
       await _context.SaveChangesAsync();
    }
}