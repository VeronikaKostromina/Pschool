using Pschool.Shared.ViewModels.StudentViewModels;

namespace Web.Services.Contracts
{
    public interface IStudentService
    {
        Task<List<StudentDetailsViewModel>> GetAll();
        Task Delete(long id);
        Task<StudentDetailsViewModel> Create(StudentDetailsViewModel studentViewModel);
        Task<StudentDetailsViewModel> Update(StudentDetailsViewModel studentViewModel);
        Task<List<StudentDetailsViewModel>> GetByParentId(long id);
    }
}
