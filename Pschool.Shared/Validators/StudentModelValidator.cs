using FluentValidation;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool.Shared.Validators
{
    public class StudentModelValidator : AbstractValidator<StudentDetailsViewModel>
    {
        public StudentModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name can not be empty");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("First name can not be more than 50 symbols");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name can not be empty");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Last name can not be more than 50 symbols");
            RuleFor(x => x.ClassNumber).ExclusiveBetween(0, 12).WithMessage("Class number should be from 1 to 11");
            RuleFor(x => x.ParentId).NotEmpty().NotEqual(0).WithMessage("Student`s parent can not be empty");
        }
    }
}
