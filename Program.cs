using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace vaccine_watch
{
    class Program
    {
        private static IConfigurationRoot configuration;
        static async Task Main(string[] args)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddHttpClient()
                .AddSingleton<IAvailabilityCheck, AvailabilityCheck>()
                .AddSingleton<IVaccineAlert, VaccineAlert>()
                .AddSingleton<IConfigurationRoot>(configuration)
                .BuildServiceProvider();

            var checker = serviceProvider.GetService<IAvailabilityCheck>();
            var check = await checker.GetAvailability();

            if(check.Count > 0)
            {
                var alert = serviceProvider.GetService<IVaccineAlert>();
                alert.SendAlert(check);
            }
        }
    }
}
