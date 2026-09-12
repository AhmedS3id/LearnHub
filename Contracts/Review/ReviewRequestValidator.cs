namespace LearnHub_Api.Contracts.Review;
public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
{
    public ReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(1000);
    }
}