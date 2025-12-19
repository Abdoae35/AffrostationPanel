using Agriculiture.BLL.Dtos.TreeTypeDto;
using Agriculture.DAL.Models;

namespace Agriculiture.BLL.Dtos.TreeDto;

public class TreeNameAddDto
{
    
    public string TreeName { get; set; }
    public string TreeTypeName { get; set; }
    public string? ScientificName { get; set; }
    
    
}