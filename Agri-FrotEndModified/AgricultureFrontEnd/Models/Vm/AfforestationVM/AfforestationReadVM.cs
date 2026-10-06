namespace AgricultureFrontEnd.Models.Vm.AfforestationVM;

public class AfforestationReadVM
{
    public int Id { get; set; }
    public DateOnly DateOfPlanted { get; set; }
    public string? TreeName { get; set; }
    public string? TreeTypeName { get; set; }

    
    public string? UserName { get; set; }
    public string? ScientificName { get; set; }
    
    
    public string? LocationName { get; set; }
    
    public int Number { get; set; }
}