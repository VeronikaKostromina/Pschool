namespace Pschool.Shared.Models
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
