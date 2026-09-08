namespace LearnHub_Api.Contracts.User
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
           
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 200);

        }
    }
}
