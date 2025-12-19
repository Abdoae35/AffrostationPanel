
namespace Agriculiture.BLL.Manager.TreeTypeManager;

public class TreeTypeManager : ITreeTypeManager
{
    public readonly ITreeTypeRepo _repo;

    public TreeTypeManager(ITreeTypeRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<TreeTypeReadDto>> GetAllTreeType()
    {
       var trees= await _repo.GetAll().Select(a=> new TreeTypeReadDto
       {
           Id = a.Id,
           Type = a.Type
           
       }).ToListAsync();
       
       return trees;
    }

    public async Task<TreeTypeReadDto?> GetTreeById(int id)
    {
        var treeType = await _repo.GetById(id);
        if (treeType == null) return null;
        TreeTypeReadDto treeTypeDto = new TreeTypeReadDto()
        {
            Id = treeType.Id,
            Type = treeType.Type
        };
        return treeTypeDto;
    }

    public async Task<bool> DeleteTreeType(int id)
    {
       var  treeType = await _repo.GetById(id);
       
       if (treeType == null)
            return false;
       
       await  _repo.DeleteAsync(treeType);
         return true;
    }

    public async Task AddNewType(TreeTypeAddDto treeTypeAddDto)
    {
        var newTreeType = new TreeType
        {
            Type = treeTypeAddDto.Type
        };
       await _repo.InsertAsync(newTreeType);
    }

    public async Task<bool> UpdateTree(TreeTypeUpdateDto treeDto)
    {
        var treeType = await _repo.GetById(treeDto.Id);
        if (treeType == null) return false;

        if (string.IsNullOrWhiteSpace(treeDto.Type)) return false;

        treeType.Type = treeDto.Type;
        await _repo.Update(treeType);
        return true;
    }

}