namespace Agriculiture.BLL.Dtos;

public class AfforestationUpdateDto
{
    public int Id { get; set; }
    public DateOnly? DateOfPlanted { get; set; }
    public int? TreeTypeId { get; set; }
    public int? TreeNameId { get; set; }
    public int? LocationId { get; set; }
    public int? LocationTypeId { get; set; }
    public int? Number { get; set; }
    
    
}