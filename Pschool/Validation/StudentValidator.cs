using FluentValidation.Results;
using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Validation
{
    public class StudentValidator : IValidator<Student>
    {
        private readonly IRepository<Parent> parentRepository;

        public StudentValidator(IRepository<Parent> parentRepository)
        {
            this.parentRepository = parentRepository;
        }

        public async Task<ValidationResult> CanCreateAsync(Student student)
        {
            var exists = await parentRepository.AnyAsync(x => x.Id == student.ParentId);

            if (exists)
            {
                return new ValidationResult();
            }

            return new ValidationResult(new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(student.ParentId), "Parent with ParentId is not found.")
            });
        }

        public async Task<ValidationResult> CanUpdateAsync(Student student)
        {
            return await CanCreateAsync(student);
        }
    }
}
