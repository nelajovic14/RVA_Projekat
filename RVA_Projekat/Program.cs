using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using RVA_Projekat.Dogadjaji;

namespace RVA_Projekat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILoggerManager>();
            logger.LogInformation(new Model.Dogadjaj { korisnik = "POCETAK", dogadjaj = "POCETAK", poruka = "POCETAK" });

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
