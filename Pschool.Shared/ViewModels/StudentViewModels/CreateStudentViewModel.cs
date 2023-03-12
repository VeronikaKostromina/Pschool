
namespace Pschool.Shared.ViewModels.StudentViewModels
{
    public class CreateStudentViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ClassNumber { get; set; }

        public long ParentId { get; set; }
        public string? Email { get; set; }

    }
}
