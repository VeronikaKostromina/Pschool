using FluentValidation;
using LanguageExt.Common;
using Pschool.Contracts;
using Pschool.Shared.Models;

namespace Pschool.Managers
{
    public class ParentManager : IParentManager
    {
        private readonly IRepository<Parent> parentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly Validation.IValidator<Parent> parentValidator;

        public ParentManager(
            IRepository<Parent> parentRepository,
            IUnitOfWork unitOfWork,
            Validation.IValidator<Parent> parentValidator)
        {
            this.parentRepository = parentRepository ?? throw new ArgumentNullException(nameof(parentRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.parentValidator = parentValidator;
        }

        public async Task<Result<Parent>> Create(Parent parent)
        {
            var validationResult = await parentValidator.CanCreateAsync(parent);
            if (validationResult.IsValid == false)
            {
                return new Result<Parent>(new ValidationException(validationResult.Errors));
            }

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

        public async Task<Result<Parent>> Update(Parent parent)
        {
            var validationResult = await parentValidator.CanUpdateAsync(parent);
            if (validationResult.IsValid == false)
            {
                return new Result<Parent>(new ValidationException(validationResult.Errors));
            }

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
