using Pschool.Shared.ViewModels.ParentViewModels;

namespace Web.Services.Contracts
{
    public interface IParentService
    {
        Task<List<ParentViewModel>> GetAll();
        Task Delete(long id);
        Task<ParentViewModel> Create(ParentViewModel parentViewModel);
        Task<ParentViewModel> Update(ParentViewModel parentViewModel);
    }
}
