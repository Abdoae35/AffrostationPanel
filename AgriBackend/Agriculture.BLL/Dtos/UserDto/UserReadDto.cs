namespace Agriculiture.BLL.Dtos.UserDto;

public class UserReadDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}