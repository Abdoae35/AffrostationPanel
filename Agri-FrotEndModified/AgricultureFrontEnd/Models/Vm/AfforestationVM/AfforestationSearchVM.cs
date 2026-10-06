using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationSearchVM
{
    public List<TreeSummaryReadVM> LastMonthSummary { get; set; } = new();
    [Required(ErrorMessage = "From Date cannot be empty.")]
    public DateOnly? FromDate { get; set; }
    [Required(ErrorMessage = "To Date cannot be empty.")]
    public DateOnly? ToDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int? SelectedUserId { get; set; }
    public int? SelectedLocationId { get; set; }
    public int? SelectedTreeId { get; set; }

    public List<SelectListItem>? Users { get; set; }
    public List<SelectListItem>? Locations { get; set; }
    public List<SelectListItem>? Trees { get; set; }
    public List<TypeSummaryVM>? TypeSummary { get; set; }
    
}