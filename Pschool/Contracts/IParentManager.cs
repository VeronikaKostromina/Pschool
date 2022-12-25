using LanguageExt.Common;
using Pschool.Shared.Models;

namespace Pschool.Contracts
{
    public interface IParentManager
    {
        Task<Result<Parent>> Create(Parent parent);
        Task Remove(long key);
        Task<Result<Parent>> Update(Parent parent);

        IQueryable<Parent> FindAll();
    }
}
