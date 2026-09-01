using LearnHub_Api.Authentication.Filter;
using LearnHub_Api.Contracts.Category;
using LearnHub_API.Abstractions.Consts;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("")]
        [HasPermission(Permissions.GetCategories)]
        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllAsync( cancellationToken);
            return Ok(result);
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddCategories)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request,CancellationToken cancellationToken)
        {
            var result = await _categoryService.CreateAsync(request, cancellationToken);
            return  result.IsSuccess? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) 
                : result.ToProblem();
        }
        [HttpGet("{id}")]
        [HasPermission(Permissions.GetCategories)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateCategories)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(id, request,cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeleteCategories)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
      
}
