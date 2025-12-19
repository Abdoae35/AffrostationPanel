namespace Agriculture.DAL.Models;

public class TreeType
{
    public int Id { get; set; }
    public string Type { get; set; }
    
    public ICollection<TreeName> TreeNames { get; set; }

       
}