namespace Agriculture.DAL.Repository.UserRepo;

public class UserRepo : IUserRepo
{
    private readonly AgriContext _context;
    public UserRepo( AgriContext context)
    {
        _context = context;
    }
    
    public IQueryable<User> GetAllUsers()
    {
       return _context.Users.AsNoTracking();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public void InsertUser(User user)
    {
       _context.Users.Add(user);
       _context.SaveChanges();
    }

    public async Task Update(User user)
    {
         _context.Users.Update(user);
         await _context.SaveChangesAsync();
    }


    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

   
}