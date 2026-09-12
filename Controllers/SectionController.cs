using LearnHub_Api.Authentication.Filter;
using LearnHub_Api.Contracts.Section;
using LearnHub_API.Abstractions.Consts;
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
        [HasPermission(Permissions.AddSections)]
        public async Task<IActionResult> Create([FromRoute] int courseId ,SectionRequest request,CancellationToken cancellationToken)
        {
            var result = await _sectionService.CreateAsync(courseId,request,cancellationToken);
            return result.IsSuccess? CreatedAtAction(nameof(GetById),
                new { courseId, sectionId = result.Value.Id },
                            result.Value)
                        : result.ToProblem();
        }
        [HttpGet("course/{courseId}")]
        [HasPermission(Permissions.GetSections)]
        public async Task<IActionResult> GetAll([FromRoute] int courseId ,CancellationToken cancellationToken)
        {
            var result = await _sectionService.GetAllAsync(courseId,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("course/{courseId}/section/{sectionId}")]
        [HasPermission(Permissions.GetSections)]
        public async Task<IActionResult> GetById([FromRoute] int courseId, [FromRoute] int sectionId, CancellationToken cancellationToken)
        {
            var result = await _sectionService.GetByIdAsync(courseId, sectionId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPut("course/{courseId}/section/{sectionId}")]
        [HasPermission(Permissions.UpdateSections)]
        public async Task<IActionResult> Update([FromRoute] int courseId, [FromRoute] int sectionId,SectionRequest request, CancellationToken cancellationToken)
        {
            var result = await _sectionService.UpdateAsync(courseId, sectionId,request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("course/{courseId}/section/{sectionId}")]
        [HasPermission(Permissions.DeleteSections)]
        public async Task<IActionResult> Delete([FromRoute] int courseId, [FromRoute] int sectionId, CancellationToken cancellationToken)
        {
            var result = await _sectionService.DeleteAsync(courseId, sectionId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
