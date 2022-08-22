using log4net;
using log4net.Config;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RVA_Projekat.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace RVA_Projekat.Dogadjaji
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
        public LoggerManager()
        {
            try
            {
                XmlDocument log4netConfig = new XmlDocument();

                using (var fs = File.OpenRead("log4net.config"))
                {
                    log4netConfig.Load(fs);
                    var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
                    XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
                    _logger.Info("Log System Initialized");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error", ex);
            }
        }
        public void LogInformation(Dogadjaj d)
        {
            string message = d.korisnik + ":" + d.dogadjaj + ":" + d.poruka;
            _logger.Info(message);
        }
        public List<Dogadjaj> GetInfo(string username)
        {
            List<Dogadjaj> info = new List<Dogadjaj>();
            string[] text = File.ReadAllLines(@"C:\Users\Korisnik\Desktop\RVA_Projekat\RVA_Projekat\Logs\logs.txt2022-08-21.txt");
            foreach(string line in text)
            {
                if ((line.Split(':')[0]).Trim() == username)
                {
                    info.Add(new Dogadjaj { dogadjaj= line.Split(':')[1] , poruka = line.Split(':')[2], korisnik = line.Split(':')[0] });
                }
            }
            return info;  
        }
    }
    
}
