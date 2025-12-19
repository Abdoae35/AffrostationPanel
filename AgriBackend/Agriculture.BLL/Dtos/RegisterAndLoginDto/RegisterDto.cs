namespace Agriculiture.BLL.Dtos.RegisterAndLoginDto;

public class RegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}