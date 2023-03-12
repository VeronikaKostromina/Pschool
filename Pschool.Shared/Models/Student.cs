namespace Pschool.Shared.Models
{
    public class Student : BaseEntity<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int ClassNumber { get; set; }
        public string? Email { get; set; }

        public long ParentId { get; set; }
        public Parent? Parent { get; set; }
        public Document? Document { get; set; }

    }
}
