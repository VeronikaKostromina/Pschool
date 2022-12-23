using Pschool.Shared.ViewModels.StudentViewModels;

namespace Web.Services.Contracts
{
    public interface IStudentService
    {
        Task<List<StudentViewModel>> GetAll();
        Task Delete(long id);
        Task<StudentViewModel> Create(StudentViewModel studentViewModel);
        Task<StudentViewModel> Update(StudentViewModel studentViewModel);
        Task<List<StudentViewModel>> GetByParentId(long id);
    }
}
