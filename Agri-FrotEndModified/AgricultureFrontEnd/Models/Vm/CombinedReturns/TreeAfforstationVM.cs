using AgricultureFrontEnd.Models.Vm.AfforestationVM;

namespace AgricultureFrontEnd.Models.Vm.CombinedReturns;

public class TreeAfforstationVM
{
    public AfforstationUpdateVM? AfforstationUpdateVm { get; set; }
    public List<TreeReadVM>? Trees { get; set; }
    public List<LocationNameReadVM>? Locations { get; set; }
    public List<TreeTypeReadVM>? Types { get; set; }
    public List<LocationTypeReadVm>? LocationTypes { get; set; }
}