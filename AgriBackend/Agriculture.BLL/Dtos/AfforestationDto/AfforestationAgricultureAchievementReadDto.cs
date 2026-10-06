namespace Agriculiture.BLL.Dtos;

public class AfforestationAgricultureAchievementReadDto
{
    public int Id { get; set; }
    public DateOnly DateOfPlanted { get; set; }
    public string? TreeName { get; set; }
    public int TreeNameId { get; set; }
    
    public string? ScientificName { get; set; }
    public string? TreeTypeName { get; set; }
    public int TreeTypeId { get; set; }
    
    public int LocationId { get; set; }
    
    public int LocationTypeId { get; set; }
    public string? LocationTypeName { get; set; }
    public string? LocationName { get; set; }

    public string? UserName { get; set; }
    
   
    public int Number { get; set; }
}