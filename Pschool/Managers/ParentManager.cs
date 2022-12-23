using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class ParentManager : IParentManager
    {
        private readonly IRepository<Parent> parentRepository;
        private readonly IUnitOfWork unitOfWork;

        public ParentManager(IRepository<Parent> parentRepository, IUnitOfWork unitOfWork)
        {
            this.parentRepository = parentRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Parent> Create(Parent entity)
        {
            await parentRepository.AddAsync(entity);
            await unitOfWork.SaveAsync();
            return entity;
        }

        public async Task Remove(long key)
        {
            await parentRepository.RemoveAsync(key);
            await unitOfWork.SaveAsync();
        }

        public async Task<Parent> Update(Parent entity)
        {
            parentRepository.Update(entity);
            await unitOfWork.SaveAsync();
            return entity;
        }

        public IQueryable<Parent> FindAll()
        {
            return parentRepository.FindAll();
        }
    }
}
