namespace Agriculiture.BLL.Manager.UserManager;

public class UserManager : IUserManager
{
    private readonly IUserRepo _repo;

    public UserManager(IUserRepo repo)
    {
        _repo = repo;
    }

    public async Task Update(UserUpdateDto user)
    {
        var userToUpdate = await _repo.GetUserByIdAsync(user.Id);

        if (userToUpdate != null)
        {
            userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _repo.Update(userToUpdate);
        }
    }

    public async Task<string> RegisterAsync(RegisterDto dto, Roles role)
    {
        var existingUser = await _repo.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Email already registered.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword,
            Role = role
        };

        await _repo.AddAsync(user);
        return "Registration successful.";
    }

    public Task<string> LoginAsync(LoginDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<User> ValidateUserAsync(LoginDto dto)
    {
        var user = await _repo.GetByEmailAsync(dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        return user;
    }

    public IEnumerable<UserReadDto> GetAll()
    {
        return _repo.GetAllUsers().Select(a => new UserReadDto
        {
            Id = a.Id,
            Email = a.Email,
            Name = a.Name,
            Role = a.Role,
            Password=a.Password,
        }).ToList();
    }

    public UserReadDto? GetById(int id)
    {
        var user = _repo.GetUserById(id);
        
        if(user == null)
            return null;
        return new UserReadDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            Password = user.Password,
            Role = user.Role,
        };
    }

    public void Insert(UserAddDto user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = new User
        {
            Email = user.Email,
            Name = user.Name,
            Password = hashedPassword,  
            Role = user.Role
        };
        _repo.InsertUser(newUser);
    }


    public async Task DeleteAsync(int id)
    {
        var userToDelete = await _repo.GetUserByIdAsync(id);

        if (userToDelete == null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        await _repo.DeleteUserAsync(userToDelete);
    }



   
}
