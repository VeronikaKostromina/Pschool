using System.IO;
using AzureTrigger.Email;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureTrigger.Startup))]
namespace AzureTrigger
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder.AddJsonFile(
                    Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                    optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddSingleton<ISendEmail, SendEmail>();

            builder.Services.AddOptions<EmailConfiguration>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(EmailConfiguration)).Bind(settings);
                });
        }
    }
}
