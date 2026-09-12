namespace LearnHub_Api.Contracts.Lesson
{
    public class LessonRequestValidator : AbstractValidator<LessonRequest>
    {
        public LessonRequestValidator()
        {
            RuleFor(x => x.Title)
           .NotEmpty()
           .Length(5, 500);

            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(10, 1000);

            RuleFor(x => x.VideoUrl)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0);

            RuleFor(x => x.Order)
                .GreaterThan(0);
        }
    }
}
