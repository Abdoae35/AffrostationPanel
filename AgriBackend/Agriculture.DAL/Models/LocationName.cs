namespace Agriculture.DAL.Models;

public class LocationName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TypeOfLocation LocationType { get; set; }
    public int LocationTypeId { get; set; }
}