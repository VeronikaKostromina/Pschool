using FluentValidation;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class StudentManager : IStudentManager
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly Validation.IValidator<Student> studentValidator;

        public StudentManager(
            IRepository<Student> studentRepository,
            IUnitOfWork unitOfWork, Validation.IValidator<Student> studentValidator)
        {
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
            this.studentValidator = studentValidator;
        }

        public async Task<Result<Student>> Create(Student student)
        {
            var validationResult = await studentValidator.CanCreateAsync(student);
            if (validationResult.IsValid == false)
            {
                return new Result<Student>(new ValidationException(validationResult.Errors));
            }

            student.Created = DateTime.UtcNow;
            student.Updated = DateTime.UtcNow;

            await studentRepository.AddAsync(student);
            await unitOfWork.SaveAsync();

            return student;
        }

        public async Task Remove(long key)
        {
            await studentRepository.RemoveAsync(key);
            await unitOfWork.SaveAsync();
        }

        public async Task<Result<Student>> Update(Student student)
        {
            var validationResult = await studentValidator.CanCreateAsync(student);
            if (validationResult.IsValid == false)
            {
                return new Result<Student>(new ValidationException(validationResult.Errors));
            }

            student.Updated = DateTime.UtcNow;

            studentRepository.Update(student);
            await unitOfWork.SaveAsync();

            return student;
        }

        public IQueryable<Student> FindAll()
        {
            return studentRepository.FindAll().Include(x => x.Parent);
        }

        public IQueryable<Student> FindByParent(long key)
        {
            return studentRepository.Where(x => x.ParentId == key).Include(x => x.Parent);
        }
    }
}
