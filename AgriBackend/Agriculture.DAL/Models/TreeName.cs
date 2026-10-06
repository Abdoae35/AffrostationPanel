namespace Agriculture.DAL.Models;

public class TreeName
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public TreeType Type { get; set; }
    public int TypeId { get; set; }
    
    
    public string? ScientificName { get; set; }
}