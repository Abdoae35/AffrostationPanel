using AgricultureFrontEnd.Models.Vm.AfforestationVM;
using AgricultureFrontEnd.Models.Vm.CombinedReturns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class AfforestationController : Controller
{
    private readonly HttpClient _client;

    public AfforestationController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("http://agricultureachievement.runasp.net/");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, string returnUrl = null)
    {
        var record = await _client.GetFromJsonAsync<AfforstationUpdateVM>($"api/afforestation/{id}");
        var trees = await _client.GetFromJsonAsync<List<TreeReadVM>>("Tree/GetAll");
        var locations = await _client.GetFromJsonAsync<List<LocationNameReadVM>>("Location/GetAll");
        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
        var locationTypes = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");

        var vm = new TreeAfforstationVM
        {
            AfforstationUpdateVm = record,
            Trees = trees,
            Locations = locations,
            Types = types,
            LocationTypes = locationTypes
        };

        
        ViewBag.ReturnUrl = returnUrl ?? Url.Action("GetStatistics", "Search");
        return View(vm);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(AfforstationUpdateVM dto, string returnUrl)
    {
        
      
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "بيانات غير صحيحة ❌";
            return RedirectToAction("Edit", new { id = dto.Id, returnUrl });
        }

        var response = await _client.PutAsJsonAsync($"api/afforestation/{dto.Id}", dto);

        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "تم تحديث البيانات بنجاح ✅";
            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl); 
        }

        TempData["ErrorMessage"] = "فشل تحديث البيانات ❌";
        return RedirectToAction("Edit", new { id = dto.Id, returnUrl });
    }

    public IActionResult Success() => View();
}
