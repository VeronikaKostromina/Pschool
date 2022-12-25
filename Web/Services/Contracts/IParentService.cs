using Pschool.Shared.ViewModels.ParentViewModels;

namespace Web.Services.Contracts
{
    public interface IParentService
    {
        Task<List<ParentDetailsViewModel>?> GetAll();
        Task<bool> Delete(long id);
        Task<ParentDetailsViewModel?> Create(ParentDetailsViewModel parentViewModel);
        Task<ParentDetailsViewModel?> Update(ParentDetailsViewModel parentViewModel);
    }
}
