using FluentValidation;
using FluentValidation.Validators;
using Pschool.Shared.ViewModels.ParentViewModels;

namespace Pschool.Shared.Validators
{
    public class ParentModelValidator : AbstractValidator<ParentDetailsViewModel>
    {
        public ParentModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name can not be empty");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("First name can not be more than 50 symbols");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name can not be empty");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Last name can not be more than 50 symbols");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can not be empty");
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Not a valid email address");
            RuleFor(x => x.Phone).Matches("^$|^[ 0-9]+$").WithMessage("Only numbers can be used in the phone");
        }
    }
}
