using Pschool.Shared.Models;

namespace Pschool.Contracts
{
    public interface IStudentManager
    {
        Task<Student> Create(Student entity);
        Task Remove(long key);
        Task<Student> Update(Student entity);

        IQueryable<Student> FindAll();

        IQueryable<Student> FindByParent(long key);
    }
}
