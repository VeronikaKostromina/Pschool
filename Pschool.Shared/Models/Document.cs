namespace Pschool.Shared.Models
{
    public class Document : BaseEntity<long>
    {
        public string? FileName { get; set; }
        public long StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
