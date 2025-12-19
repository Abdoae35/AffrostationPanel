namespace Agriculture.DAL.Models;

public class AfforestationAgricultureAchievement
{
    public int Id { get; set; }
    public DateOnly DateOfPlanted { get; set; }
    
    
    public TreeType TreeType { get; set; }
    public int TreeTypeId { get; set; }
    
    public TreeName TreeName { get; set; }
    public int TreeNameId { get; set; }
    
    public LocationName LocationName { get; set; }
    public int LocationNameId { get; set; }

    public TypeOfLocation LocationType { get; set; }
    public int LocationTypeId { get; set; }
    
    public int Number { get; set; }
    
    
    public User User { get; set; }
    public int UserId { get; set; }
    
    
}