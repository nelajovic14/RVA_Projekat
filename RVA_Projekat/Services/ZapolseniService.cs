using RVA_Projekat.Dto;
using RVA_Projekat.Interface.InterfaceZaposlenih;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Services
{
    public class ZapolseniService:IZaposleniService
    {
        IZaposleniRepository zapolseniRepository;

        public ZapolseniService(IZaposleniRepository zapolseniRepository)
        {
            this.zapolseniRepository = zapolseniRepository;
        }

        public Zaposleni DodajEntitet(ZaposleniDto dto)
        {
            Zaposleni zaposleni=new Zaposleni { Ime=dto.Ime, GodineIskustva=dto.GodineIskustva, BrutoHonorarId=dto.BrutoHonorarId };
            zapolseniRepository.Add(zaposleni);
            return zaposleni;
        } 

        public void Dodaj(Zaposleni zaposleni)
        {
            zapolseniRepository.Add(zaposleni);
        }

        public Zaposleni Edit(ZaposleniDto zaposleniDto)
        {
            Zaposleni zaposleni = zapolseniRepository.Find(zaposleniDto.Id);
            zaposleni.BrutoHonorarId = zaposleniDto.BrutoHonorarId;
            zaposleni.GodineIskustva = zaposleniDto.GodineIskustva;
            zaposleni.Ime = zaposleniDto.Ime;
            Zaposleni z= zapolseniRepository.Edit(zaposleni);
            return z;
        }

        public Zaposleni Get(ZaposleniDto dto)
        {
            return zapolseniRepository.Find(dto.Id);
        }

        public List<Zaposleni> GetAll()
        {
            return zapolseniRepository.GetAll();
        }

        public void Obrisi(Zaposleni zaposleni)
        {
            zapolseniRepository.Remove(zaposleni);
        }
    }
}
