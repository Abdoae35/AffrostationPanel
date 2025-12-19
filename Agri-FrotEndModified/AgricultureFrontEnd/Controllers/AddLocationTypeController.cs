using AgricultureFrontEnd.Models.Vm.LocationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class AddLocationTypeController : Controller
{
    private readonly HttpClient _client;

    public AddLocationTypeController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7197/");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");

        var model = new LocationTypePageVM
        {
            AddModel = new LocationTypeAddVM(),
            AllTypes = types ?? new List<LocationTypeReadVm>()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationTypePageVM model)
    {
        if (string.IsNullOrWhiteSpace(model.AddModel.LocationType))
        {
            return BadRequest("Location Type Name cannot be empty.");
        }

        await _client.PostAsJsonAsync("LocationType/AddNewType", model.AddModel);
        var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
        return PartialView("_LocationTypeTable", types);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
      var respose=  await _client.DeleteAsync($"LocationType/Delete/{id}");
      if (respose.IsSuccessStatusCode)
      {
          var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
            return PartialView("_LocationTypeTable", types);
          
      }
      return Json(new { success = false, message = "❌ النوع مرتبط بسجل، احذف السجل أولا" });
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] LocationTypeUpdateVM model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.LocationType))
            return BadRequest("Location Type cannot be empty.");

        await _client.PutAsJsonAsync("LocationType/UpdateLocationType", model);

        var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
        return PartialView("_LocationTypeTable", types);
    }


}