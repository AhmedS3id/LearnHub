namespace LearnHub_Api.Contracts.Course
{
    public class CourseRequestValidator : AbstractValidator<CourseRequest>
    {
        public CourseRequestValidator()
        {
            RuleFor(x=>x.Title)
                .NotEmpty()
                .Length(5,500);
            RuleFor(x=>x.Description)
                .NotEmpty()
                .Length(10,1000);
            RuleFor(x => x.Price)
                .GreaterThan(0);
            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
