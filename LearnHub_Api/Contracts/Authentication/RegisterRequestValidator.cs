namespace LearnHub_Api.Contracts.Authentication
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
                
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexPattern.Password)
                .WithMessage("Password must be at least 8 digits and must contain LowerCase,UpperCase,Numbers,NonAlphabetic \"");

            RuleFor(x => x.ConfirmPassword)
               .Equal(x => x.Password)
               .WithMessage("Passwords do not match.");
        }
    }
}
