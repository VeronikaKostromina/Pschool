using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class ParentManager : IParentManager
    {
        private readonly IRepository<Parent> parentRepository;
        private readonly IUnitOfWork unitOfWork;

        public ParentManager(
            IRepository<Parent> parentRepository,
            IUnitOfWork unitOfWork)
        {
            this.parentRepository = parentRepository ?? throw new ArgumentNullException(nameof(parentRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Parent> Create(Parent parent)
        {
            parent.Created = DateTime.UtcNow;
            parent.Updated = DateTime.UtcNow;

            await parentRepository.AddAsync(parent);
            await unitOfWork.SaveAsync();
            return parent;
        }

        public async Task Remove(long key)
        {
            await parentRepository.RemoveAsync(key);
            await unitOfWork.SaveAsync();
        }

        public async Task<Parent> Update(Parent parent)
        {
            parent.Updated = DateTime.UtcNow;
            parentRepository.Update(parent);
            await unitOfWork.SaveAsync();
            return parent;
        }

        public IQueryable<Parent> FindAll()
        {
            return parentRepository.FindAll();
        }
    }
}
