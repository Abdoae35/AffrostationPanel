namespace Agriculiture.BLL.Dtos.TreeDto;

public class TreeReadDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int TypeId { get; set; }
    public string? ScientificName { get; set; }
}