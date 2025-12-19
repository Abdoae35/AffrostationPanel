using Agriculiture.BLL.Dtos.TreeDto;
using Agriculture.DAL.Models;

namespace Agriculiture.BLL.Manager.TreeManager;

public interface ITreeNameManager
{
    Task<List<TreeReadDto>> GetAllTrees();
    Task<TreeReadDto?> GetTreeById(int id);
    Task<List<TreeReadDto>?> GetByTypeId(int id);
    Task<bool> DeleteTree(int id);
    void AddNewTree(TreeNameAddDto treeDto);
    Task<bool> UpdateTree(TreeUpdateDto tree);
}