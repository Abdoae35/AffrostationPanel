using Agriculiture.BLL.Dtos.LocationDto;
using Agriculture.DAL.Models;

namespace Agriculiture.BLL.Manager.LocationManager;

public interface ILocationManager
{
    Task<List<LocationNameReadDto>> GetAllLocationsAsync();
    Task<List<LocationNameReadDto>?> GetByTypeId(int id);
    Task<LocationNameReadDto?> GetLocationByIdAsync(int id);
    Task<bool> DeleteLocationAsync(int id);
    Task AddNewLocationAsync(LocationNameAddDto location);
    Task UpdateLocationAsync(LocationNameUpdateDto location);
}