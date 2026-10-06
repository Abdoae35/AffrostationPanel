namespace Agriculiture.BLL.Manager.UserManager;

public interface IUserManager
{
    public IEnumerable<UserReadDto> GetAll();
    public UserReadDto GetById(int id);
    public void Insert(UserAddDto user);
    public Task DeleteAsync(int id);
    public Task Update(UserUpdateDto user);
    public Task<string> RegisterAsync(RegisterDto dto, Roles role);
    public Task<string> LoginAsync(LoginDto dto);
    public Task<User> ValidateUserAsync(LoginDto dto);



}