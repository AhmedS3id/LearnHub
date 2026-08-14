using LearnHub_Api.Contracts.Section;

namespace LearnHub_Api.Services
{
    public interface ISectionService
    {
        Task<Result<SectionResponse>> CreateAsync(int courseId, SectionRequest request, CancellationToken cancellationToken);
        Task<Result<IEnumerable<SectionResponse>>> GetAllAsync(int courseId, CancellationToken cancellationToken);
        Task<Result<SectionResponse>> GetByIdAsync(int courseId,int sectionId, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int courseId,int sectionId,SectionRequest request, CancellationToken cancellationToken);
    }
}
