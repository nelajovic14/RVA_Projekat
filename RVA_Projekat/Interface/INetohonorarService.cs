using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Interface
{
    public interface INetohonorarService
    {
        NetoHonorar DodajEntitet(NetoHonorarDto dto);
        List<NetoHonorar> GetAll();
        NetoHonorar Get(int id);
        void Obrisi(NetoHonorar netoHonorar);
    }
}
