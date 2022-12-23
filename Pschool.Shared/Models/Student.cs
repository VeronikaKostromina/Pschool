namespace Pschool.Shared.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ClassNumber { get; set; }

        public long ParentId { get; set; }
        public Parent? Parent { get; set; }

    }
}
