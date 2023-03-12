namespace Pschool.Shared.ViewModels.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ClassNumber { get; set; }
        public long ParentId { get; set; }
        public string? ParentFullName { get; set; }
        public string? Email { get; set; }

    }
}
