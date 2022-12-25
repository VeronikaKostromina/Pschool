using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Pschool.Extensions;

namespace Pschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Reply<TSource, TDestination>(Result<TSource> validationResult, Func<TSource, TDestination> mappingFunc)
        {
            return validationResult.Match<IActionResult>(x =>
            {
                return Ok(mappingFunc(x));
            }, exception =>
            {
                if (exception is ValidationException validationException)
                {
                    this.ModelState.AddValidationErrors(validationException.Errors);
                    return ValidationProblem();
                }

                return new StatusCodeResult(500);
            });
        }
    }
}
