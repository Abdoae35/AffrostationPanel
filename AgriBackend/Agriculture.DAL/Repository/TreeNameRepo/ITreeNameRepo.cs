
namespace Agriculture.DAL.Repository.TreeNameRepo;

public interface ITreeNameRepo
{
    public IQueryable<TreeName> GetAll();
    
  
    public Task<TreeName> GetById(int id);
    public IQueryable<TreeName?> GetByTypeId(int id);
    public void Insert(TreeName tree);
    
    public Task Update(TreeName tree);
    public Task Delete(TreeName tree);
    TreeType GetByTypeName(string typeName);
    

    
}