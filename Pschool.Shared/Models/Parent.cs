namespace Pschool.Shared.Models
{
    public class Parent : BaseEntity<long>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public string? Email { get; set; }
        public string? HomeAddress { get; set; }
        public string? Phone { get; set; }
    }
}
