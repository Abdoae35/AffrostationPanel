namespace Agriculture.DAL.Models;

public class TypeOfLocation
{
    public int Id { get; set; }
    public string LocationType { get; set; }
    
    public ICollection<LocationName> LocationNames { get; set; }
}