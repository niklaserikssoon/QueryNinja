using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueryNinja.Data;
using QueryNinja.UI;
using QueryNinja.Service;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace QueryNinja
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                var ui = host.Services.GetRequiredService<UserInterFace>();
                ui.DisplayUI();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An application error occurred during startup:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<QueryNinjasDbContext>();
                    services.AddTransient<ReportService>();
                    services.AddTransient<UserInterFace>();
                });
    }
}