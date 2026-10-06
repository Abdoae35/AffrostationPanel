using Agriculiture.BLL.Dtos.TreeTypeDto;

namespace Agriculiture.BLL.Manager.TreeTypeManager;

public interface ITreeTypeManager
{
    Task<List<TreeTypeReadDto>> GetAllTreeType();
    Task<TreeTypeReadDto?> GetTreeById(int id);
    Task<bool> DeleteTreeType(int id);
    Task AddNewType(TreeTypeAddDto treeTypeAddDto);
    Task<bool>? UpdateTree(TreeTypeUpdateDto treeDto);
}