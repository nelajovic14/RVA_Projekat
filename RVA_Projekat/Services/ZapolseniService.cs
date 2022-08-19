using RVA_Projekat.Interface;
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

        public void Dodaj(Zaposleni zaposleni)
        {
            zapolseniRepository.Add(zaposleni);
        }

        public Zaposleni Get(int id)
        {
            return zapolseniRepository.Find(id);
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
