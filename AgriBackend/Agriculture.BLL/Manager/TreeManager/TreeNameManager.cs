using Agriculiture.BLL.Dtos.TreeDto;
using Agriculture.DAL.Models;
using Agriculture.DAL.Repository.TreeNameRepo;
using Agriculture.DAL.Repository.TreeTypeRepo;
using Microsoft.EntityFrameworkCore;

namespace Agriculiture.BLL.Manager.TreeManager;

public class TreeNameManager : ITreeNameManager
{
    private readonly ITreeNameRepo _treeNameRepo;
    private readonly ITreeTypeRepo _treeTypeRepo;

    public TreeNameManager(ITreeNameRepo treeNameRepo, ITreeTypeRepo treeTypeRepo)
    {
        _treeNameRepo = treeNameRepo;
        _treeTypeRepo = treeTypeRepo;
    }

    public async Task<List<TreeReadDto>> GetAllTrees()
    {
        var allTrees = _treeNameRepo.GetAll()
            .Include(a => a.Type)
            .Select(a => new TreeReadDto()
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type.Type,
                TypeId = a.Type.Id,
                ScientificName = a.ScientificName
            }).ToList();

        return await Task.FromResult(allTrees); 
    }

    public async Task<TreeReadDto?> GetTreeById(int id)
    {
        var tree = await _treeNameRepo.GetById(id);
        if(tree == null)
            return null;
        var treeDto = new TreeReadDto()
        {
            Id = tree.Id,
            Name = tree.Name,
            TypeId = tree.TypeId,
            Type = tree.Type.Type,
            ScientificName = tree.ScientificName
            
        };
        return treeDto;
    }

    public async Task<List<TreeReadDto>?> GetByTypeId(int id)
    {
       var typeExist = await _treeTypeRepo.GetById(id);
       
       if (typeExist == null)
           return null;
       
        
       return await  _treeNameRepo.GetByTypeId(id).Select(a=> new TreeReadDto()
        {
            Id = a.Id,
            Name = a.Name,
            TypeId = a.TypeId,
            Type = a.Type.Type,
            ScientificName = a.ScientificName
        }).ToListAsync();

    }

    public async Task<bool> DeleteTree(int id)
    {
        var treeDto = await _treeNameRepo.GetById(id);
        
        //Delete Failed
        if (treeDto == null) 
            return false;
        
        //Delete Success
        await _treeNameRepo.Delete( treeDto);
        return true;


    }

    public void AddNewTree(TreeNameAddDto treeDto)
    {
        if (string.IsNullOrWhiteSpace(treeDto.TreeTypeName))
            throw new ArgumentException("TreeTypeName is required.");

        var existingType = _treeNameRepo.GetByTypeName(treeDto.TreeTypeName);

        if (existingType == null)
        {
            existingType = new TreeType
            {
                Type = treeDto.TreeTypeName
            };
            _treeTypeRepo.InsertAsync(existingType);
        }

        var newTree = new TreeName
        {
            Name = treeDto.TreeName,
            TypeId = existingType.Id,
            ScientificName = treeDto.ScientificName
            
        };

        _treeNameRepo.Insert(newTree);
    }

    public async Task<bool> UpdateTree(TreeUpdateDto treedto)
    {
        var tree = await _treeNameRepo.GetById(treedto.Id);
        if (tree == null) return false;

       
        if (!string.IsNullOrWhiteSpace(treedto.Name))
            tree.Name = treedto.Name;

        if (!string.IsNullOrWhiteSpace(treedto.ScientificName))
            tree.ScientificName = treedto.ScientificName;

        await _treeNameRepo.Update(tree);
        return true;
    }

}
