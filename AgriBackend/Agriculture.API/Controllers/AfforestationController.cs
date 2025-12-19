namespace Agriculture.API.Controllers;
    [ApiController]
    [Route("api/afforestation")]
    public class AfforestationController : ControllerBase
    {
        private readonly IAfforestationAgricultureAchievementManager _manager;

        public AfforestationController(IAfforestationAgricultureAchievementManager manager)
        {
            _manager = manager;
        }
        
        

        [HttpPost("search")]
        public IActionResult Search( [FromBody] AfforestationSearchRequest request)
        {
            var result = _manager.BetterSearch(request);
            return Ok(result);
        }
        
        
        [HttpPost("PaginatedSearch")]
        public IActionResult Search([FromBody] AfforestationSearchRequest request, int page = 1, int pageSize = 10)
        {
            var result = _manager.PaginatedSearch(request, page, pageSize);
            return Ok(result);
        }

        
        

        [HttpPost("Add")]
        public async Task<IActionResult> Insert([FromBody] AfforestationAddDto afforestationAddDto)
        {
            await _manager.insert(afforestationAddDto);
            return Ok(afforestationAddDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _manager.GetByIdAsynco(id);
            if (model == null)
                return NotFound(new { success = false, message = "Record not found" });
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _manager.GetByIdAsynco(id);
            if (model == null)
                return NotFound();
            
            await _manager.Delete(model);
            return Ok($"Record  deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AfforestationUpdateDto dto)
        {
            var model = await _manager.GetByIdAsynco(id);
            if (model == null)
                return NotFound(new { success = false, message = "Record not found" });

            await _manager.Update(dto);

            return Ok(new
            {
                success = true,
                message = "Record updated successfully",
                updatedData = dto
            });
        }


    }