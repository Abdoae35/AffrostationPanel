

using System.ComponentModel.DataAnnotations;

namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationFormVM
{
    
    [Required(ErrorMessage = "اختيار اسم النبات مطلوب")]
    public int TreeNameId { get; set; }

    [Required(ErrorMessage = "اختيار الموقع مطلوب")]
    public int LocationNameId { get; set; }

    [Required(ErrorMessage = "عدد النباتات مطلوب")]
    [Range(1, int.MaxValue, ErrorMessage = "عدد النباتات يجب أن يكون أكبر من 0")]
    public int? Number { get; set; }

    
    public DateTime DateOfPlanted { get; set; } = DateTime.Now;

    public List<TreeReadVM>? TreeNames { get; set; }
    public List<LocationNameReadVM>? LocationNames { get; set; }
    
    public List<string>? TreeTypes { get; set; }
    public List<string>? LocationTypes { get; set; }
}