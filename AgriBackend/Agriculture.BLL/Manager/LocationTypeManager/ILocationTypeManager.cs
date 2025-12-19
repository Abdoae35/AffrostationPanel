

namespace Agriculiture.BLL.Manager.LocationTypeManager;

public interface ILocationTypeManager
{
    Task<List<LocationTypeReadDto>> GetAllLocationType();
    Task<LocationTypeReadDto> GetLocationTypeById(int id);
    Task<bool> DeleteLocationTypeById(int id);
    Task AddNewLocationType(LocationTypeAddDto treeTypeAddDto);
    Task<bool> UpdateLocation(LocationTypeUpdateDto treeDto);
    
}