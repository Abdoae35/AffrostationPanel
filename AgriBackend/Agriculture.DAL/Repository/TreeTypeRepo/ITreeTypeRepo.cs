namespace Agriculture.DAL.Repository.TreeTypeRepo;

public interface ITreeTypeRepo
{
    public IQueryable<TreeType> GetAll();
    public Task<TreeType>GetById(int id);
    public Task InsertAsync(TreeType tree);
    public Task Update(TreeType tree);
    public Task DeleteAsync(TreeType tree);
}