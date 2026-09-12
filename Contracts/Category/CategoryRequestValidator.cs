namespace LearnHub_Api.Contracts.Category
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(5, 1000);

        }
    }
}
