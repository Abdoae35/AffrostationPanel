namespace AgricultureFrontEnd.Models.Vm.LocationVm;

public class LocationTypePageVM
{
    public LocationTypeAddVM AddModel { get; set; } = new();
    public List<LocationTypeReadVm> AllTypes { get; set; } = new();
}