using Pschool.Shared.Models;

namespace Pschool.Contracts
{
    public interface IParentManager
    {
        Task<Parent> Create(Parent entity);
        Task Remove(long key);
        Task<Parent> Update(Parent entity);

        IQueryable<Parent> FindAll();
    }
}
