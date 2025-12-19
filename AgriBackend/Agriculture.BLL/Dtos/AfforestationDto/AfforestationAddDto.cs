namespace Agriculiture.BLL.Dtos;

public class AfforestationAddDto
{
    
   
    public DateOnly DateOfPlanted { get; set; }
    public int TreeTypeId { get; set; }
    public int TreeNameId { get; set; }
    public int LocationNameId { get; set; }
    public int LocationTypeId { get; set; }
    
    public int UserId { get; set; }
    public int Number { get; set; }

}