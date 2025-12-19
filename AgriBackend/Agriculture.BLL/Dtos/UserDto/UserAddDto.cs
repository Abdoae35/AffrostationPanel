
namespace Agriculiture.BLL.Dtos.UserDto;

public class UserAddDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}