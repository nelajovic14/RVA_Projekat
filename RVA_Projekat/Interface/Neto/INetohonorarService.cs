using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Interface.Neto
{
    public interface INetohonorarService:IDupliranje
    {
        NetoHonorar DodajEntitet(NetoHonorarDto dto);
        List<NetoHonorar> GetAll();
        NetoHonorar Get(NetoHonorarDto dto);
        void Obrisi(NetoHonorar netoHonorar);
        NetoHonorar Dodaj(NetoHonorar neto);
        NetoHonorar Edit(NetoHonorarDto dto);
    }
}
