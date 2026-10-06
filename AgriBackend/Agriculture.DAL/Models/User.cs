namespace Agriculture.DAL.Models;

public class User
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
    
    public ICollection<AfforestationAgricultureAchievement>  AfforestationAchievements { get; set; }
}