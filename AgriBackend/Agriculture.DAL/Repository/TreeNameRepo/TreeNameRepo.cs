namespace Agriculture.DAL.Repository.TreeNameRepo;
public class TreeNameRepo : ITreeNameRepo
{
    private readonly AgriContext _context;

    public TreeNameRepo(AgriContext context)
    {
        _context = context;
    }

    public IQueryable<TreeName> GetAll()
    {
        
        return _context.TreeNames
            .Include(a => a.Type)
            .OrderBy(a=>a.Name).AsNoTracking();
    }

    public async Task<TreeName?> GetById(int id)
    {
        return await _context.TreeNames
            .Include(a => a.Type)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public IQueryable<TreeName?> GetByTypeId(int id)
    {
        return _context.TreeNames
            .Include(a => a.Type)
            .Where(a => a.TypeId == id)
            .AsNoTracking();
    }


    public void Insert(TreeName tree)
    {
        _context.TreeNames.Add(tree);
        _context.SaveChanges();
    }

    public async Task Update(TreeName tree)
    {
        _context.TreeNames.Update(tree);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TreeName tree)
    {
        _context.TreeNames.Remove(tree);
        await _context.SaveChangesAsync();
    }

    public TreeType? GetByTypeName(string typeName)
    {
        return _context.TreeTypes
            .FirstOrDefault(t => t.Type == typeName);
    }
}
