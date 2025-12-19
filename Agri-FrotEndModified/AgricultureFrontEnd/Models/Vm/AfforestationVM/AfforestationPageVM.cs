namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationPageVM
{
    public List<AfforestationReadVM>? TreeDetails { get; set; }
    public List<TreeSummaryReadVM>? TreeSummaries { get; set; }
    public List<TypeSummaryVM>? TreeTypeSummaries { get; set; } = new();
    
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
  

}


