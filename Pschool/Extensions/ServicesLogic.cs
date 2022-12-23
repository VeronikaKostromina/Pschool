using FluentValidation;
using Pschool.Contracts;
using Pschool.Managers;
using Pschool.Shared.Validators;
using Pschool.Shared.ViewModels.ParentViewModels;
using Pschool.Shared.ViewModels.StudentViewModels;

namespace Pschool.Extensions
{
    public static class ServicesLogic
    {
        public static IServiceCollection AddServicesLogic(this IServiceCollection services)
        {
            services.AddScoped<IStudentManager, StudentManager>();
            services.AddScoped<IParentManager, ParentManager>();

            services.AddScoped<IValidator<ParentViewModel>, ParentModelValidator>();
            services.AddScoped<IValidator<StudentViewModel>, StudentModelValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
