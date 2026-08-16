using LearnHub_Api.Contracts.User;

namespace LearnHub_Api.Contracts.User
{
    public class UpdateProfileUserRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileUserRequestValidator()
        {
           
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 200);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 200);

        }
    }
}
