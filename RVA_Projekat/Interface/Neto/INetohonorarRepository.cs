using RVA_Projekat.Model;
using System;

namespace RVA_Projekat.Interface.Neto
{
    public interface INetohonorarRepository : IRepository<NetoHonorar>
    {
        NetoHonorar Edit(NetoHonorar nh);
        DateTime GetLastChange(int id);
    }
}
