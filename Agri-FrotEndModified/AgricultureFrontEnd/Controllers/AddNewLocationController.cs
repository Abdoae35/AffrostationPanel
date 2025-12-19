using AgricultureFrontEnd.Models.Vm.LocationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Controllers;


[Authorize(Roles = "Admin")]
public class AddNewLocationController : Controller
{
   private readonly HttpClient _client;

   public AddNewLocationController(HttpClient client)
   {
      _client = client;
      _client.BaseAddress = new Uri("https://localhost:7197/");
   }



   public async Task<IActionResult> Create()
   {
      var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
      
      ViewBag.TypesList = new SelectList(types, "LocationType", "LocationType");
      
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> Create(LocationAddVm model)
   {
      if (!ModelState.IsValid)
      {
         var types = await _client.GetFromJsonAsync<List<LocationTypeReadVm>>("LocationType/GetAll");
         ViewBag.TypesList = new SelectList(types, "LocationType", "LocationType");

      }

      var payload = new
      {
         name = model.Name,
         locationType = model.LocationType
      };
      var response= await _client.PostAsJsonAsync("Location/Add", payload);
      if (response.IsSuccessStatusCode)
      {
         TempData["Success"] = "✅ تمت اضافه الموقع بنجاح";
         return RedirectToAction("Create");
      }

      TempData["Error"] = "❌ فشل في اضافه الموقع";
      return RedirectToAction("Create");
   }
   
   [HttpPost]
   public async Task<IActionResult> AddLocation([FromBody] LocationAddVm model)
   {
      if (!ModelState.IsValid)
      {
         return Json(new { success = false, message = "البيانات غير صحيحة" });
      }

      var payload = new
      {
         Name = model.Name,
         LocationType = model.LocationType
      };

      var response = await _client.PostAsJsonAsync("Location/Add", payload);

      if (!response.IsSuccessStatusCode)
      {
         return Json(new { success = false, message = "فشل API" });
      }

      LocationNameReadVM? createdLocation = null;
      try
      {
         createdLocation = await response.Content.ReadFromJsonAsync<LocationNameReadVM>();
      }
      catch
      {
         // ignore if API didn’t return JSON
      }

      // If API didn’t return object, fallback to model
      if (createdLocation == null)
      {
         return Json(new
         {
            success = true,
            data = new
            {
               id = 0, // or generate a temp id if needed
               Name = model.Name,
               LocationType = model.LocationType
            }
         });
      }

      return Json(new
      {
         success = true,
         data = new
         {
            id = createdLocation.Id,
            Name = createdLocation.Name,
            LocationType = createdLocation.LocationType
         }
      });
   }
   
  

}