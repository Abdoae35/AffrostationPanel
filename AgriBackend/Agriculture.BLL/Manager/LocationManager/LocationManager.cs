using Agriculiture.BLL.Dtos.LocationDto;
using Agriculture.DAL.Models;
using Agriculture.DAL.Repository.LocationNameRepo;
using Agriculture.DAL.Repository.TypeOfLocationRepo;
using Microsoft.EntityFrameworkCore;

namespace Agriculiture.BLL.Manager.LocationManager
{
    public class LocationManager : ILocationManager
    {
        private readonly ILocationNameRepo _locationNameRepo;
        private readonly ITypeOfLocationRepo _typeOfLocationRepo;

        public LocationManager(ILocationNameRepo locationNameRepo, ITypeOfLocationRepo typeOfLocationRepo)
        {
            _locationNameRepo = locationNameRepo;
            _typeOfLocationRepo = typeOfLocationRepo;
        }

        public async Task<List<LocationNameReadDto>> GetAllLocationsAsync()
        {
            var locations = await _locationNameRepo
                .GetAll()
                .Select(a => new LocationNameReadDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    LocationType = a.LocationType.LocationType,
                    LocationTypeId = a.LocationTypeId
                    
                })
                .ToListAsync();

            return locations;
        }

        public async Task<List<LocationNameReadDto>?> GetByTypeId(int id)
        {
            var typeExist = await _typeOfLocationRepo.GetByIdAsync(id);
            if (typeExist == null)
                return null;
            
            
            var selectedLocations =  _locationNameRepo.GetByTypeId(id).Select(
                a=>new LocationNameReadDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    LocationType = a.LocationType.LocationType,
                    LocationTypeId = a.LocationTypeId
                }).ToListAsync();
            return await selectedLocations;
        }

        public async Task<LocationNameReadDto?> GetLocationByIdAsync(int id)
        {
           var location = await _locationNameRepo.GetByIdAsync(id);
           if (location != null)
           {
               LocationNameReadDto? dto = new  LocationNameReadDto()
               {
                   Id = location.Id,
                   Name = location.Name,
                   LocationTypeId = location.LocationTypeId,
                  LocationType = location.LocationType.LocationType
               };
               return dto;
           }
           return null;
        }

        public async Task<bool> DeleteLocationAsync(int id)
        {
            var location = await _locationNameRepo.GetByIdAsync(id);
            
            
            if (location == null)
                return false;
            
            
            await _locationNameRepo.DeleteLocationAsync(location);
            return true;


        }

        public async Task AddNewLocationAsync(LocationNameAddDto location)
        {
            if (string.IsNullOrWhiteSpace(location.LocationType))
                throw new ArgumentException("Location Type is required.");

            var existingType = _locationNameRepo.GetByTypeName(location.LocationType);

            if (existingType == null)
            {
                existingType = new TypeOfLocation
                {
                    LocationType = location.LocationType
                };

                await _typeOfLocationRepo.InsertAsync(existingType);
            }

            var newLocation = new LocationName
            {
                Name = location.Name,
                LocationTypeId = existingType.Id
            };

            await _locationNameRepo.InsertLocationAsync(newLocation);
        }

        public async Task UpdateLocationAsync(LocationNameUpdateDto location)
        {
            

            var existing = await _locationNameRepo.GetByIdAsync(location.Id);
            if (existing == null)
                throw new Exception($"Location with ID {location.Id} not found.");
            //Check if not Null change
            if(!string.IsNullOrWhiteSpace(location.Name))
                existing.Name = location.Name;
            

            await _locationNameRepo.UpdateAsync(existing);
        }
    }
}
