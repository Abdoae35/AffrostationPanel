namespace Agriculture.API.Controllers;

    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _manager;

        public LocationController(ILocationManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LocationNameAddDto dto)
        {
            await _manager.AddNewLocationAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _manager.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           

           var respose = await _manager.DeleteLocationAsync(id);
           if (respose)
               return Ok($"Location with {id} has been deleted.");
           else
               return NotFound("$Location with {id}  not found.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var locatoin = await _manager.GetLocationByIdAsync(id);
            if (locatoin == null)
                return NotFound();
            return Ok(locatoin);
        }

        [HttpGet("{locationTypeId}")]
        public async Task<IActionResult> GetByLocationTypeId(int locationTypeId)
        {
            var locations = await _manager.GetByTypeId(locationTypeId);
            if (locations == null)
                return NotFound();
            return Ok(locations);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationNameUpdateDto dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest("Invalid Location data.");
            await _manager.UpdateLocationAsync(dto);
            return Ok($"Locatoin Name : {dto.Name} has been updated.");
            
        }
    }
