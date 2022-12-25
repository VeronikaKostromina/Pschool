using Pschool.Contracts;
using Pschool.Managers;
using Pschool.Shared.Models;
using Pschool.Validation;

namespace Pschool.Extensions
{
    public static class ServicesLogicExtensions
    {
        public static IServiceCollection AddServicesLogic(this IServiceCollection services)
        {
            services.AddScoped<IStudentManager, StudentManager>();
            services.AddScoped<IParentManager, ParentManager>();

            services.AddScoped<IValidator<Parent>, ParentValidator>();
            services.AddScoped<IValidator<Student>, StudentValidator>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
