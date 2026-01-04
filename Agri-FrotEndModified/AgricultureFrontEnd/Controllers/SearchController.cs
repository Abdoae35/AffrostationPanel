using AgricultureFrontEnd.Models.Vm.AfforestationVM;
using AgricultureFrontEnd.Models.Vm.UsersVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using System.IO;




namespace AgricultureFrontEnd.Controllers 
{
    [Authorize(Roles = "Admin,Viewer")]
    public class SearchController : Controller
    {
        private readonly HttpClient _client;
        

        public SearchController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7197/");
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var model = new AfforestationSearchVM
            {
                Users = await LoadUsersAsync(),
                Locations = await LoadLocationsAsync(),
                Trees = await LoadTreesAsync(),
            };

            var last30DaysFrom = DateOnly.FromDateTime(DateTime.Today.AddDays(-30));
            var today = DateOnly.FromDateTime(DateTime.Today);

            var summaryRequest = new AfforestationSearchVM
            {
                FromDate = last30DaysFrom,
                ToDate = today
            };

            var response = await _client.PostAsJsonAsync("api/afforestation/search", summaryRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<AfforestationReadVM>>();

                if (result != null)
                {
                    model.LastMonthSummary = result
                        .GroupBy(a => a.TreeName)
                        .Select(t => new TreeSummaryReadVM
                        {
                            TreeName = t.Key,
                            TotalPlanted = t.Sum(a => a.Number)
                        }).ToList();

                    model.TypeSummary = result
                        .GroupBy(a => a.TreeTypeName ?? "Unknown")
                        .Select(t => new TypeSummaryVM
                        {
                            TypeName = t.Key,
                            Count = t.Sum(a => a.Number)
                        }).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStatistics(AfforestationSearchVM model, int page = 1, int pageSize = 15)
        {
            model.Users = await LoadUsersAsync();
            model.Locations = await LoadLocationsAsync();
            model.Trees = await LoadTreesAsync();

            if (model.FromDate == null || model.ToDate == null)
            {
                ModelState.AddModelError("", "Please select both dates.");
                return View(model);
            }

            var response = await _client.PostAsJsonAsync($"api/afforestation/Search", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Server error, please try again later.");
                return View(model);
            }

            var allData = await response.Content.ReadFromJsonAsync<List<AfforestationReadVM>>();
           

            if (allData == null)
            {
                ModelState.AddModelError("", "No data received from server.");
                return View(model);
            }
            
            var pageVm = new AfforestationPageVM
            {
                TreeDetails = allData,  // send all results to front-end
                CurrentPage = 1,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allData.Count / (double)pageSize)
            };

            HttpContext.Session.SetString("Duration",JsonSerializer.Serialize(model));
           
            HttpContext.Session.SetString("ExportData", JsonSerializer.Serialize(allData));

            return View("SearchResults", pageVm);
        }

        [HttpPost]
        public async Task<IActionResult> ExportDatatoExcel()
                        {
                            var json = HttpContext.Session.GetString("ExportData");
                            var durationJson = HttpContext.Session.GetString("Duration");

                            if (string.IsNullOrEmpty(durationJson))
                                return BadRequest("Duration is Null");

                            if (string.IsNullOrEmpty(json))
                                return BadRequest("No data to export.");

                            var duration = JsonSerializer.Deserialize<AfforestationSearchVM>(durationJson);
                            var treeDetails = JsonSerializer.Deserialize<List<AfforestationReadVM>>(json);

                            if (treeDetails == null || !treeDetails.Any())
                                return BadRequest("No data available to export.");

                            await using var stream = new MemoryStream();

                            // Run ClosedXML operations in a background thread to avoid blocking
                            await Task.Run(() =>
                            {
                                using var workbook = new ClosedXML.Excel.XLWorkbook();
                                var worksheet = workbook.Worksheets.Add("Afforestation Data");

                                // Header row
                                worksheet.Cell(1, 1).Value = "Date Planted";
                                worksheet.Cell(1, 2).Value = "Plant Name";
                                worksheet.Cell(1, 3).Value = "Scientific Name";
                                worksheet.Cell(1, 4).Value = "Plant Type";
                                worksheet.Cell(1, 5).Value = "Location Name";
                                worksheet.Cell(1, 6).Value = "Number";
                                worksheet.Cell(1, 7).Value = "User Name";

                                // Data rows
                                int row = 2;
                                foreach (var item in treeDetails)
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
                            });

                            stream.Seek(0, SeekOrigin.Begin);

                            var fileName = $"Afforestation({duration.FromDate?.ToString("dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture)}) " 
                                           +
                                           $"to ({duration.ToDate?.ToString("dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture)}).xlsx";

                            return File(
                                stream.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                fileName
                            );
                        }


        

        [HttpPost]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var response = await _client.DeleteAsync($"api/afforestation/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, id = id });
            }

            return Json(new { success = false, message = "Failed to delete record." });
        }

        private async Task<List<SelectListItem>> LoadUsersAsync()
        {
            var users = await _client.GetFromJsonAsync<List<UserReadVM>>("User/GetAll") ?? new();

            var normalUsers = users
                .Where(u => u.Role == Roles.User || u.Role== Roles.Admin)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }).ToList();

            normalUsers.Insert(0, new SelectListItem { Value = "", Text = "All Users" });
            return normalUsers;
        }

        private async Task<List<SelectListItem>> LoadLocationsAsync()
        {
            var locations = await _client.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll") ?? new();
            var items = locations.Select(l => new SelectListItem { Value = l.Id.ToString(), Text = l.Name }).ToList();
            items.Insert(0, new SelectListItem { Value = "", Text = "All Locations" });
            return items;
        }

        private async Task<List<SelectListItem>> LoadTreesAsync()
        {
            var trees = await _client.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll") ?? new();
            var items = trees.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name }).ToList();
            items.Insert(0, new SelectListItem { Value = "", Text = "All Trees" });
            return items;
        }
    }
}
