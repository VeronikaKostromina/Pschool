using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pschool.Extensions;

public static class ValidationResultExtensions
{
    public static void AddValidationErrors(this ModelStateDictionary modelState, IEnumerable<ValidationFailure> errors)
    {
        foreach (var error in errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}