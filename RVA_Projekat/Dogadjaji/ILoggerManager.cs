using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Dogadjaji
{
    public interface ILoggerManager
    {
        void LogInformation(Dogadjaj d);
        List<Dogadjaj> GetInfo(string username);
    }
}
