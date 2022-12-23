using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Pschool.Extensions;

namespace Pschool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MSSQL")));
            services.AddScoped<DbContext, ApplicationContext>();
            services.AddServicesLogic();

            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(x => x.AddProfile(new AutoMapperProfile()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
