using Microsoft.AspNetCore.Authorization;

namespace Agriculture.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TreeController : ControllerBase
{
    private readonly ITreeNameManager _treeNameManager;

    public TreeController(ITreeNameManager treeNameManager)
    {
        _treeNameManager = treeNameManager;
    }

    [HttpPost]
    public IActionResult Add([FromBody] TreeNameAddDto nameAddTreeName)
    {
        _treeNameManager.AddNewTree(nameAddTreeName);
        return Ok();
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trees = await _treeNameManager.GetAllTrees();
        return Ok(trees);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       

       var respose= await _treeNameManager.DeleteTree(id);
       
        
        if(respose)
         return Ok($"Tree with {id} deleted successfully.");
        else
            return NotFound($"Tree with {id} not found.");
        
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tree = await _treeNameManager.GetTreeById(id);
        if (tree == null)
            return NotFound();
        return Ok(tree);
    }

    [HttpGet("{typeId}")]
    public async Task<IActionResult> GetByTypeId(int typeId)
    {
        var trees =  await _treeNameManager.GetByTypeId(typeId);
        if (trees == null)
        {
            return NotFound("Type is not exist");
        }
        if (trees.Count == 0)
            return NotFound("Type is exist but no tree Assigned to");
        
            
        return Ok(trees);
        
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTree(TreeUpdateDto treeUpdateDto)
    {
        if (treeUpdateDto == null || treeUpdateDto.Id <= 0)
            return BadRequest("Invalid tree data.");

        var updated = await _treeNameManager.UpdateTree(treeUpdateDto);

        if (!updated)
            return NotFound($"Tree with ID {treeUpdateDto.Id} not found.");

        return Ok("Tree updated successfully.");
    }
    

    
}