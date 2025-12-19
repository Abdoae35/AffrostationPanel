

namespace Agriculture.API.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class LocationTypeController : ControllerBase
{
   private readonly ILocationTypeManager _manager;

   public LocationTypeController(ILocationTypeManager manager)
   {
      _manager = manager;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var result = await _manager.GetAllLocationType();
      return Ok(result);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
   {
      var result = await _manager.GetLocationTypeById(id);
      return Ok(result);
   }

   [HttpPost]
   public async Task<IActionResult> AddNewType(LocationTypeAddDto locationTypeAddDto)
   {
      await _manager.AddNewLocationType(locationTypeAddDto);
      return Ok($"LocationType {locationTypeAddDto.LocationType}");
      
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
      var respose = await _manager.DeleteLocationTypeById(id);
      if(respose)
         return Ok($"LocationType {id} deleted");
      else
         return NotFound($"LocationType {id} not found");
   }
   [HttpPut]
   public async Task<IActionResult> UpdateLocationType( LocationTypeUpdateDto locationTypeDto)
   {
      if (locationTypeDto == null || string.IsNullOrWhiteSpace(locationTypeDto.LocationType))
         return BadRequest("location type cannot be null or empty.");

      var updated = await _manager.UpdateLocation(locationTypeDto);

      if (!updated)
         return NotFound($"Location type with ID {locationTypeDto.Id} not found.");

      return Ok($"Location {locationTypeDto.LocationType} Updated");
   }
   
   
   
   
   
}