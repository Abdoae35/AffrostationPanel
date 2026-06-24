using Microsoft.AspNetCore.Authorization;

namespace Agriculture.API.Controllers;
    [Authorize]
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

        [HttpPost("export")]
        public IActionResult ExportToExcel([FromBody] AfforestationSearchRequest request)
        {
            var data = _manager.BetterSearch(request);

            if (data == null || !data.Any())
                return BadRequest("No data found for the given filters.");

            using var stream = new MemoryStream();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Afforestation Data");

                worksheet.Cell(1, 1).Value = "Date Planted";
                worksheet.Cell(1, 2).Value = "Plant Name";
                worksheet.Cell(1, 3).Value = "Scientific Name";
                worksheet.Cell(1, 4).Value = "Plant Type";
                worksheet.Cell(1, 5).Value = "Location Name";
                worksheet.Cell(1, 6).Value = "Number";
                worksheet.Cell(1, 7).Value = "User Name";

                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.DateOfPlanted.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 2).Value = item.TreeName;
                    worksheet.Cell(row, 3).Value = item.ScientificName;
                    worksheet.Cell(row, 4).Value = item.TreeTypeName;
                    worksheet.Cell(row, 5).Value = item.LocationName;
                    worksheet.Cell(row, 6).Value = item.Number;
                    worksheet.Cell(row, 7).Value = item.UserName;
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(stream);
            }

            stream.Seek(0, SeekOrigin.Begin);

            var fromStr = request.FromDate?.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) ?? "All";
            var toStr = request.ToDate?.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) ?? "All";
            var fileName = $"Afforestation({fromStr}) to ({toStr}).xlsx";

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }


    }