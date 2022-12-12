using FluentValidation;
using Sat.Recruitment.Api.Requests;

namespace Sat.Recruitment.Api.Validators.Users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserNameIsRequired);

            RuleFor(user => user.Email).ValidEmail();

            RuleFor(user => user.Address)
                .NotEmpty()
                .WithMessage(ErrorMessages.AddressIsRequired);

            RuleFor(user => user.Phone)
                .NotEmpty()
                .WithMessage(ErrorMessages.PhoneIsRequired);
        }
    }
}