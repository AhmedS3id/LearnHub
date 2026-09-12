namespace LearnHub_Api.Contracts.Section;
public class SectionRequestValidator : AbstractValidator<SectionRequest>
{
    public SectionRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Order)
            .GreaterThan(0);
    }
}