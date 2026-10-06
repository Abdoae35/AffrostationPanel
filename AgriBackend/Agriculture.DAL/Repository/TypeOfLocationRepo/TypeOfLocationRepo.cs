namespace Agriculture.DAL.Repository.TypeOfLocationRepo;

public class TypeOfLocationRepo : ITypeOfLocationRepo
{
    private readonly AgriContext _context;

    public TypeOfLocationRepo(AgriContext context)
    {
        _context = context;
    }

    public async Task<List<TypeOfLocation>> GetAllAsync()
    {
        return await _context.TypeOfLocations.OrderBy(a=>a.LocationType)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TypeOfLocation?> GetByIdAsync(int id)
    {
        return await _context.TypeOfLocations.FindAsync(id);
    }

    public async Task InsertAsync(TypeOfLocation entity)
    {
        await _context.TypeOfLocations.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TypeOfLocation entity)
    {
        _context.TypeOfLocations.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TypeOfLocation entity)
    {
        _context.TypeOfLocations.Remove(entity);
        await _context.SaveChangesAsync();
    }
}