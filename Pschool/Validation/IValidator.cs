using FluentValidation.Results;
using Pschool.Shared.Models;

namespace Pschool.Validation
{
    public interface IValidator<T> where T : BaseEntity<long>
    {
        Task<ValidationResult> CanCreateAsync(T entity);
        Task<ValidationResult> CanUpdateAsync(T entity);
    }
}
