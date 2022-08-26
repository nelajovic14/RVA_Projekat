using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using System;
using System.Collections.Generic;

namespace RVA_Projekat.Interface.Neto
{
    public interface INetohonorarService
    {
        NetoHonorar DodajEntitet(NetoHonorarDto dto);
        List<NetoHonorar> GetAll();
        NetoHonorar Get(NetoHonorarDto dto);
        bool Obrisi(NetoHonorar netoHonorar, DateTime izmena);
        NetoHonorar Dodaj(NetoHonorar neto);
        NetoHonorar Edit(NetoHonorarDto dto);
        NetoHonorar Dupliraj(NetoHonorarDto honorar);
    }
}
