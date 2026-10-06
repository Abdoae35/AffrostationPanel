using Agriculture.DAL.Repository.TypeOfLocationRepo;

namespace Agriculiture.BLL.Manager.LocationTypeManager;

public class LocationTypeManager : ILocationTypeManager
{
    private readonly ITypeOfLocationRepo _repo;

    public LocationTypeManager(ITypeOfLocationRepo repo)
    {
        _repo = repo;
    }
    public async Task<List<LocationTypeReadDto>> GetAllLocationType()
    {
        var types = await _repo.GetAllAsync();

        return types.Select(a => new LocationTypeReadDto
        {
            Id = a.Id,
            LocationType = a.LocationType
            
        }).ToList();
       
    }


    public async Task<LocationTypeReadDto?> GetLocationTypeById(int id)
    {
       var locaionType = await _repo.GetByIdAsync(id);
       if (locaionType != null)
       {
           var locationDto = new LocationTypeReadDto()
           {
               Id = locaionType.Id,
               LocationType = locaionType.LocationType
           };
           return locationDto;
       }

       return null;
    }

    public async Task<bool> DeleteLocationTypeById(int id)
    {
        var deleteLocationType = await _repo.GetByIdAsync(id);
        
        //Delete Failed
        if (deleteLocationType == null)
            return false;
        
        
        //Delete Success
        await _repo.DeleteAsync(deleteLocationType);
        return true;

    }

    public async Task AddNewLocationType(LocationTypeAddDto locationTypeAddDto)
    {
        var newTypeLocation = new TypeOfLocation()
        {
            LocationType = locationTypeAddDto.LocationType
        };

        await _repo.InsertAsync(newTypeLocation);

    }

    public async Task<bool> UpdateLocation(LocationTypeUpdateDto locationTypedto)
    {
        var model = await _repo.GetByIdAsync(locationTypedto.Id);
        if (model == null) return false;

        if (string.IsNullOrWhiteSpace(locationTypedto.LocationType)) return false;

        model.LocationType = locationTypedto.LocationType;
        await _repo.UpdateAsync(model);
        return true;
    }
}