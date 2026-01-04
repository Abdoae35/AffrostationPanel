using AgricultureFrontEnd.Models.Vm.TreeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureFrontEnd.Controllers;

[Authorize(Roles = "Admin")]
public class AddTreeTypeController : Controller
{
    private readonly HttpClient _client;

    public AddTreeTypeController(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7197/");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");

        var model = new TreeTypePageVM
        {
            AddModel = new TreeTypeAddVM(),
            AllTypes = types ?? new List<TreeTypeReadVM>()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TreeTypePageVM model)
    {
        if (string.IsNullOrWhiteSpace(model.AddModel.Type))
        {
            return BadRequest("Tree Type Name cannot be empty.");
        }

        await _client.PostAsJsonAsync("TreeType/AddNewType", model.AddModel);

        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
        return PartialView("_TreeTypeTable", types ?? new List<TreeTypeReadVM>());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var respose =  await _client.DeleteAsync($"TreeType/DeleteTypes/{id}");
        if(respose.IsSuccessStatusCode)
        {
            var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
            return PartialView("_TreeTypeTable", types ?? new List<TreeTypeReadVM>());
            
        }
        return Json(new { success = false, message = "❌ النوع مرتبط بسجل، احذف السجل أولا" });
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] TreeTypeUpdateVM model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Type))
            return BadRequest("Plant Type Name cannot be empty.");

        await _client.PutAsJsonAsync("TreeType/UpdateType", model);

        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
        return PartialView("_TreeTypeTable", types ?? new List<TreeTypeReadVM>());
    }

}