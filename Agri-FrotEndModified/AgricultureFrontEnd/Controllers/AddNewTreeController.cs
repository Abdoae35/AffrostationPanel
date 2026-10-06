using AgricultureFrontEnd.Models.Vm.TreeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Controllers;


[Authorize(Roles = "Admin")]
public class AddNewTreeController : Controller
{
    private readonly HttpClient  _client;

    public AddNewTreeController(HttpClient  client)
    {
        _client = client; 
    }

    public async Task<IActionResult> Create()
    {
        var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");

        
        ViewBag.TypeList = new SelectList(types, "Type", "Type");

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(TreeAddVm model)
    {
        if (!ModelState.IsValid)
        {
            var types = await _client.GetFromJsonAsync<List<TreeTypeReadVM>>("TreeType/GetAllTreeType");
            ViewBag.TypeList = new SelectList(types, "Type", "Type");
            return View(model);
        }

        var payload = new
        {
            treeName = model.Name,
            treeTypeName = model.TreeType ,
            ScientificName = model.ScientificName
            
        };

        var response = await _client.PostAsJsonAsync("/Tree/Add", payload);  

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "🌿 تمت اضافه النبات بنجاح";
            return RedirectToAction("Create");
        }

        TempData["Error"] = "❌ فشل في اضافه النبات";
        return RedirectToAction("Create");
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTree([FromBody] TreeAddVm model)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "البيانات غير صحيحة" });
        }

        var payload = new
        {
            treeName = model.Name,
            treeTypeName = model.TreeType,
            scientificName = model.ScientificName
        };

        var response = await _client.PostAsJsonAsync("Tree/Add", payload);

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "فشل API" });
        }

        TreeReadVM? createdTree = null;
        try
        {
            createdTree = await response.Content.ReadFromJsonAsync<TreeReadVM>();
        }
        catch
        {
            // ignore if API didn’t return JSON
        }

        // If API didn’t return object, fallback to model
        if (createdTree == null)
        {
            return Json(new
            {
                success = true,
                data = new
                {
                    id = 0, // or generate a temp id if needed
                    name = model.Name,
                    scientificName = model.ScientificName
                }
            });
        }

        return Json(new
        {
            success = true,
            data = new
            {
                id = createdTree.Id,
                name = createdTree.Name,
                scientificName = createdTree.ScientificName
            }
        });
    }



    
    


    
   
}