using LearnHub_Api.Contracts.Section;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SectionController(ISectionService sectionService) : ControllerBase
    {
        private readonly ISectionService _sectionService = sectionService;

        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId ,SectionRequest request,CancellationToken cancellationToken)
        {
            var result = await _sectionService.CreateAsync(courseId,request,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAll([FromRoute] int courseId ,CancellationToken cancellationToken)
        {
            var result = await _sectionService.GetAllAsync(courseId,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
