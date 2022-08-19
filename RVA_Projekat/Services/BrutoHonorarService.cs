using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Services
{
    public class BrutoHonorarService:IBrutoHonorarService
    {
        IBrutoHonorarRepository repository;
        public BrutoHonorarService(IBrutoHonorarRepository repository)
        {
            this.repository = repository;
        }

        public void Delete(BrutoHonorar brutoHonorar)
        {
            repository.Remove(brutoHonorar);
        }

        public BrutoHonorar DodajEntitet(BrutoHonorarDto dto)
        {
            
            BrutoHonorar bh = new BrutoHonorar(dto.TrenutnaPlata, Enums.Valuta.RSD);
            if (dto.valuta == "KM")
            {
                bh.valuta = Valuta.KM;
            }
            else if (dto.valuta == "EUR")
            {
                bh.valuta = Valuta.EUR;
            }
            if (GetByPlataIValuta(bh.TrenutnaPlata, bh.valuta) != null)
            {
                return GetByPlataIValuta(bh.TrenutnaPlata, bh.valuta);
            }
            repository.Add(bh);
            return bh;
        }

        public List<BrutoHonorar> GetAll()
        {
            return repository.GetAll();
        }

        public BrutoHonorar GetById(int id)
        {
            return repository.Find(id);
        }

        public BrutoHonorar GetByPlataIValuta(int plata, Valuta v)
        {
            List<BrutoHonorar> brutoHonorars = GetAll();
            foreach(var bh in brutoHonorars)
            {
                if(bh.valuta == v && bh.TrenutnaPlata == plata)
                {
                    return bh;
                }
            }
            return null;
        }
    }
}
