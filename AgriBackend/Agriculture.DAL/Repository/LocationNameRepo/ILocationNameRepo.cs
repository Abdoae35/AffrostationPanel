

using Microsoft.EntityFrameworkCore.Query;

namespace Agriculture.DAL.Repository.LocationNameRepo;

public interface ILocationNameRepo
{
    IQueryable<LocationName> GetAll();
    IQueryable<LocationName?> GetByTypeId(int typeId);
    Task<LocationName?> GetByIdAsync(int id);
    Task InsertLocationAsync(LocationName location);
    Task UpdateAsync(LocationName location);
    Task DeleteLocationAsync(LocationName location);
    TypeOfLocation GetByTypeName(string typeName);
    
}