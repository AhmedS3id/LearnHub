namespace LearnHub_Api.Common
{
    public class RequestFilterValidator : AbstractValidator<RequestFilter>
    {
        public RequestFilterValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);
        }
    }
}
