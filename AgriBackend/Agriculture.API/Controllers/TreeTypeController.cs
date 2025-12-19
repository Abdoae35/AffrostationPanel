using Agriculiture.BLL.Dtos.TreeTypeDto;
using Agriculiture.BLL.Manager.TreeTypeManager;

namespace Agriculture.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TreeTypeController : ControllerBase
{
    private readonly ITreeTypeManager _manager;

    public TreeTypeController(ITreeTypeManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTreeType()
    {
        var trees= await _manager.GetAllTreeType();
        return Ok(trees);
    }

    [HttpPost]
    public async Task<IActionResult> AddNewType([FromBody] TreeTypeAddDto treeDto)
    {
        await _manager.AddNewType(treeDto);
        return Ok($"Type {treeDto.Type} Added");
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteTypes(int id)
    {
       var response = await _manager.DeleteTreeType(id);
       if(response)
            return Ok($"Type {id} Deleted");
       
       return 
           NotFound($"Type {id} Not Deleted");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTreeType(int id)
    {
         var tree= await _manager.GetTreeById(id);
         if (tree == null)
             return NotFound();
         return Ok(tree);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateType(TreeTypeUpdateDto treeDto)
    {
        if (treeDto == null || string.IsNullOrWhiteSpace(treeDto.Type))
            return BadRequest("Tree type cannot be null or empty.");

        var updated = await _manager.UpdateTree(treeDto);

        if (!updated)
            return NotFound($"Tree type with ID {treeDto.Id} not found.");

        return Ok($"Type {treeDto.Type} Updated");
    }

    
    
    
    
}