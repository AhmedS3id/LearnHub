using LearnHub_Api.Contracts.Category;
using LearnHub_Api.Errors;

namespace LearnHub_Api.Services
{
    public class CategoryService(ApplicationDbContext context) : ICategoryService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<CategoryResponse>> CreateAsync(CategoryRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _context.Categories.AnyAsync(x => x.Name == request.Name, 
                cancellationToken: cancellationToken);
            if (isExist)
                return Result.Failure<CategoryResponse>(CategoryErrors.AlreadyExists);
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
            };
            var result = _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            var response= result.Adapt<CategoryResponse>();
            return Result.Success(response);
        }
    }
}
