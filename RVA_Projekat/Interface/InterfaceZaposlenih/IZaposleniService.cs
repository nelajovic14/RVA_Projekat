using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Interface.InterfaceZaposlenih
{
    public interface IZaposleniService
    {
        List<Zaposleni> GetAll();
        Zaposleni Get(ZaposleniDto dto);
        void Obrisi(Zaposleni zaposleni);
        void Dodaj(Zaposleni zaposleni);
        Zaposleni Edit(ZaposleniDto zaposleniDto);
    }
}
