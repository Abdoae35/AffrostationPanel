using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Agriculture.DAL.Repository.LocationNameRepo
{
    public class LocationNameRepo : ILocationNameRepo
    {
        private readonly AgriContext _context;

        public LocationNameRepo(AgriContext context)
        {
            _context = context;
        }

        public IQueryable<LocationName> GetAll()
        {
            return _context.LocationNames
                .Include(a => a.LocationType)
                .OrderBy(a=>a.Name).AsNoTracking();
        }

        public IQueryable<LocationName?> GetByTypeId(int typeId)
        {
            return _context.LocationNames
                .Include(a => a.LocationType)
                .Where(t=>t.LocationTypeId == typeId)
                .OrderBy(a=>a.Name)
                .AsNoTracking();
        }

        public async Task<LocationName?> GetByIdAsync(int id)
        {
            return await _context.LocationNames
                .Include(a=>a.LocationType)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertLocationAsync(LocationName location)
        {
            await _context.LocationNames.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LocationName location)
        {
            _context.LocationNames.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocationAsync(LocationName location)
        {
            _context.LocationNames.Remove(location);
            await _context.SaveChangesAsync();
        }

        public TypeOfLocation? GetByTypeName(string typeName)
        {
            return _context.TypeOfLocations.FirstOrDefault(a => a.LocationType == typeName);
        }
    }
}