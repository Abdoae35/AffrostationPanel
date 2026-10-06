using Agriculiture.BLL.Dtos.LocationTypeDto;

namespace Agriculiture.BLL.Dtos.LocationDto;

public class LocationNameReadDto
{
   
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? LocationType  { get; set; }
    public int LocationTypeId { get; set; }
    
   
}