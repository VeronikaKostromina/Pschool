using Microsoft.AspNetCore.Components.Forms;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Web.Services.Contracts
{
    public interface IStudentService
    {
        Task<List<StudentDetailsViewModel>?> GetAll();
        Task<bool> Delete(long id);
        Task<StudentDetailsViewModel?> Create(StudentDetailsViewModel studentViewModel, IBrowserFile? file);
        Task<StudentDetailsViewModel?> Update(StudentDetailsViewModel studentViewModel);
        Task<List<StudentDetailsViewModel>?> GetByParentId(long id);
    }
}
