namespace Agriculture.DAL.Repository.TypeOfLocationRepo;

public interface ITypeOfLocationRepo
{
    Task<List<TypeOfLocation>> GetAllAsync();
    Task<TypeOfLocation?> GetByIdAsync(int id);
    Task InsertAsync(TypeOfLocation entity);
    Task UpdateAsync(TypeOfLocation entity);
    Task DeleteAsync(TypeOfLocation entity);
}