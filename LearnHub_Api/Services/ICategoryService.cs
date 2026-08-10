using LearnHub_Api.Contracts.Category;

namespace LearnHub_Api.Services
{
    public interface ICategoryService
    {
        Task <Result<CategoryResponse>>CreateAsync (CategoryRequest request ,CancellationToken cancellationToken);
    }
}
