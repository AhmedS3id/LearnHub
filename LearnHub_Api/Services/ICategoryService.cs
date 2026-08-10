using LearnHub_Api.Contracts.Category;

namespace LearnHub_Api.Services
{
    public interface ICategoryService
    {
        Task <Result<CategoryResponse>>CreateAsync (CategoryRequest request ,CancellationToken cancellationToken);
        Task <Result<CategoryResponse>> GetByIdAsync(int id ,CancellationToken cancellationToken);
        Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int id,CategoryRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id,CancellationToken cancellationToken);
    }
}
