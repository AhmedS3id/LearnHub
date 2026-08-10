using LearnHub_Api.Contracts.Category;
using LearnHub_Api.Errors;
using Microsoft.AspNetCore.Http.HttpResults;

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

            var category = request.Adapt<Category>();
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(category.Adapt<CategoryResponse>());
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken) =>
                 await _context.Categories
                .AsNoTracking()
                .ProjectToType<CategoryResponse>()
                .ToListAsync(cancellationToken);

        public async Task<Result<CategoryResponse>> GetByIdAsync( int id,CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FindAsync([id], cancellationToken);
            return category is not null ? 
                Result.Success(category.Adapt<CategoryResponse>()):
                Result.Failure<CategoryResponse>(CategoryErrors.NotFound);
        }

        public async Task<Result> UpdateAsync(int id,CategoryRequest request, CancellationToken cancellationToken)
        {
            var currentCategory = await _context.Categories
                .FindAsync([id], cancellationToken);
            if (currentCategory is null)
                return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

            var isExist = await _context.Categories.AnyAsync(x => x.Name == request.Name && x.Id != id,
                cancellationToken: cancellationToken);
            if (isExist)
                return Result.Failure(CategoryErrors.DuplicatedName);

            currentCategory.Name = request.Name;
            currentCategory.Description = request.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
