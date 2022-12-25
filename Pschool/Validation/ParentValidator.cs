using FluentValidation.Results;
using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Validation
{
    public class ParentValidator : IValidator<Parent>
    {
        private readonly IRepository<Parent> parentRepository;

        public ParentValidator(IRepository<Parent> parentRepository)
        {
            this.parentRepository = parentRepository;
        }

        public async Task<ValidationResult> CanCreateAsync(Parent parent)
        {
            var exists = await parentRepository.AnyAsync(x => x.Email == parent.Email);

            if (exists == false)
            {
                return new ValidationResult();
            }

            return new ValidationResult(new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(parent.Email), "Email Address should be unique.")
            });
        }

        public async Task<ValidationResult> CanUpdateAsync(Parent parent)
        {
            var validationResult = new ValidationResult();
            var exists = await parentRepository.AnyAsync(x => x.Id == parent.Id);

            if (exists == false)
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(parent.Id), "Parent with Id not found."));
                return validationResult;
            }

            var existsWithTheSameEmail = await parentRepository.AnyAsync(x => x.Email == parent.Email && x.Id != parent.Id);
            if (existsWithTheSameEmail)
            {
                validationResult.Errors.Add(new ValidationFailure(nameof(parent.Email), "Email Address should be unique."));
            }

            return validationResult;
        }
    }
}
