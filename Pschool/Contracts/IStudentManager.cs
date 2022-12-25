using LanguageExt.Common;
using Pschool.Shared.Models;

namespace Pschool.Contracts
{
    public interface IStudentManager
    {
        Task<Result<Student>> Create(Student entity);
        Task Remove(long key);
        Task<Result<Student>> Update(Student student);

        IQueryable<Student> FindAll();

        IQueryable<Student> FindByParent(long key);
    }
}
