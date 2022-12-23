using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class StudentManager : IStudentManager
    {
        private readonly IRepository<Student> studentRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentManager(IRepository<Student> studentRepository, IUnitOfWork unitOfWork)
        {
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Student> Create(Student entity)
        {
            await studentRepository.AddAsync(entity);
            await unitOfWork.SaveAsync();
            return entity;
        }

        public async Task Remove(long key)
        {
            await studentRepository.RemoveAsync(key);
            await unitOfWork.SaveAsync();
        }

        public async Task<Student> Update(Student entity)
        {
            studentRepository.Update(entity);
            await unitOfWork.SaveAsync();
            return entity;
        }

        public IQueryable<Student> FindAll()
        {
            return studentRepository.FindAll();
        }

        public IQueryable<Student> FindByParent(long key)
        {
            return studentRepository.Where(x => x.ParentId == key);
        }
    }
}
