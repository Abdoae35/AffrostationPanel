namespace Agriculture.DAL.Repository.AfforestationAgricultureAchievementRepo;

public class AfforestationAgricultureAchievementRepo : IAfforestationAgricultureAchievementRepo
{
    public readonly AgriContext _context;

    public AfforestationAgricultureAchievementRepo(AgriContext context)
    {
        _context = context;
    }


    public IQueryable<AfforestationAgricultureAchievement?> GetAll()
    {
       return _context.AfforestationAgricultureAchievements.AsNoTracking();
    }
    
    public IQueryable<AfforestationAgricultureAchievement> GetFiltered(
        DateOnly? fromDate,
        DateOnly? toDate,
        int? userId,
        int? locationId,
        int? treeId)
    {
        var query = _context.AfforestationAgricultureAchievements
            .Include(x => x.TreeType)
            .Include(x => x.TreeName)
            .Include(x => x.LocationName)
            .Include(x => x.LocationType)
            .Include(x => x.User)
            .OrderBy(d=>d.DateOfPlanted)
            .AsNoTracking();

        if (fromDate.HasValue)
            query = query.Where(x => x.DateOfPlanted >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.DateOfPlanted <= toDate.Value);

        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.Value);

        if (locationId.HasValue)
            query = query.Where(x => x.LocationNameId == locationId.Value);

        if (treeId.HasValue)
            query = query.Where(x => x.TreeNameId == treeId.Value);

        return query;
    }


    public async Task<AfforestationAgricultureAchievement?> GetByIdAsync(int id)
    {
        return await _context.AfforestationAgricultureAchievements
            .Include(a => a.LocationName)
            .Include(a => a.TreeName)
            .Include(a => a.User)
            .Include(a => a.TreeType)
            .Include(a => a.LocationType)
            .FirstOrDefaultAsync(a => a.Id == id);
    }


    public async Task Insert(AfforestationAgricultureAchievement? entity)
    {
         await _context.AfforestationAgricultureAchievements.AddAsync(entity);
         await _context.SaveChangesAsync(); 
    }

    public async Task Update(AfforestationAgricultureAchievement? entity)
    {
        _context.AfforestationAgricultureAchievements.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AfforestationAgricultureAchievement? entity)
    {
        _context.AfforestationAgricultureAchievements.Remove(entity);
        await _context.SaveChangesAsync();
    }
}