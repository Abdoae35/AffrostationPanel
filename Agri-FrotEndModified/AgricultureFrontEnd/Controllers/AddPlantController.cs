using AgricultureFrontEnd.Models.Vm.AfforestationVM;
using AgricultureFrontEnd.Models.Vm.TreeVm;
using AgricultureFrontEnd.Models.Vm.LocationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Security.Claims;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "User,Admin")]
public class AddPlantController : Controller
{
    private readonly HttpClient _httpClient;

    public AddPlantController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7197/");
    }

    // GET: Create form
    public async Task<IActionResult> Create()
    {
        var treeNames = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        var locationNames = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");

        var formVm = new AfforestationFormVM
        {
            TreeNames = treeNames,
            LocationNames = locationNames,
            TreeTypes = treeNames?.Select(t => t.Type).Distinct().ToList(),
            LocationTypes = locationNames?.Select(l => l.LocationType).Distinct().ToList(),
            DateOfPlanted = DateTime.Today // default today's date
        };

        return View(formVm);
    }

    // POST: Submit form
    [HttpPost]
    public async Task<IActionResult> Create(AfforestationFormVM form)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            return Unauthorized("User not logged in.");
        }

        // ✅ Enforce date constraints for normal users
        if (!User.IsInRole("Admin"))
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var minDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-14));
            var chosen = DateOnly.FromDateTime(form.DateOfPlanted.Date);

            if (chosen > today)
            {
                ModelState.AddModelError(nameof(form.DateOfPlanted), "❌ لا يمكن اختيار تاريخ في المستقبل.");
            }
            if (chosen < minDate)
            {
                ModelState.AddModelError(nameof(form.DateOfPlanted), "❌ يمكنك اختيار تاريخ من آخر أسبوعين فقط.");
            }
        }

        if (!ModelState.IsValid)
        {
            // reload lists when model is invalid
            var treeNames = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
            var locationNames = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");

            form.TreeNames = treeNames;
            form.LocationNames = locationNames;
            form.TreeTypes = treeNames?.Select(t => t.Type).Distinct().ToList();
            form.LocationTypes = locationNames?.Select(l => l.LocationType).Distinct().ToList();

            return View(form);
        }

        //  Always reload lists for mapping (POST does not include them)
        var treeNamesFull = await _httpClient.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        var locationNamesFull = await _httpClient.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
        var treeTypes = await _httpClient.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
        var locationTypes = await _httpClient.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");

        var selectedTree = treeNamesFull?.FirstOrDefault(x => x.Id == form.TreeNameId);
        var selectedLocation = locationNamesFull?.FirstOrDefault(x => x.Id == form.LocationNameId);

        var selectedTreeType = treeTypes?.FirstOrDefault(t => t.Type == selectedTree?.Type);
        var selectedLocationType = locationTypes?.FirstOrDefault(l => l.LocationType == selectedLocation?.LocationType);

        var sendData = new AfforestationAddVM
        {
            TreeNameId = form.TreeNameId,
            TreeTypeId = selectedTreeType?.Id ?? 0,
            LocationNameId = form.LocationNameId,
            LocationTypeId = selectedLocationType?.Id ?? 0,
            Number = form.Number,
            DateOfPlanted = DateOnly.FromDateTime(form.DateOfPlanted),
            UserId = userId
        };

        var response = await _httpClient.PostAsJsonAsync("api/afforestation/Add", sendData);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "✅ تمت إضافة عملية التشجير بنجاح.";
            return RedirectToAction("Create");
        }

        TempData["Error"] = "❌ فشل في إضافة عملية التشجير.";
        return RedirectToAction("Create");
    }

    public IActionResult Success()
    {
        return View();
    }
}
