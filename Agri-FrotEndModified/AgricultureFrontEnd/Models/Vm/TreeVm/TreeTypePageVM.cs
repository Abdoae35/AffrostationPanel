namespace AgricultureFrontEnd.Models.Vm.TreeVm;

public class TreeTypePageVM
{
    public TreeTypeAddVM AddModel { get; set; } = new();
    public List<TreeTypeReadVM> AllTypes { get; set; } = new();
}