using LearnHub_Api.Contracts.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost("")]
        public async Task<IActionResult> Get([FromBody] CategoryRequest request,CancellationToken cancellationToken)
        {
            var result = await _categoryService.CreateAsync(request, cancellationToken);
            return  result.IsSuccess?Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellationToken);
            return  result.IsSuccess?Ok(result.Value) : result.ToProblem();
        }
    }
}
