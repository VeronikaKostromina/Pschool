using FluentValidation;
using Pschool.Contracts;
using Pschool.Managers;
using Pschool.Shared.Validators;
using Pschool.Shared.ViewModels.ParentViewModels;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool.Extensions
{
    public static class ServicesLogicExtensions
    {
        public static IServiceCollection AddServicesLogic(this IServiceCollection services)
        {
            services.AddScoped<IStudentManager, StudentManager>();
            services.AddScoped<IParentManager, ParentManager>();

            services.AddScoped<IValidator<ParentDetailsViewModel>, ParentModelValidator>();
            services.AddScoped<IValidator<StudentDetailsViewModel>, StudentModelValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
