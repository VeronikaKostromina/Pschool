using Pschool.Shared.Models;

namespace Pschool.Contracts
{
    public interface IParentManager
    {
        Task<Parent> Create(Parent parent);
        Task Remove(long key);
        Task<Parent> Update(Parent parent);

        IQueryable<Parent> FindAll();
    }
}
