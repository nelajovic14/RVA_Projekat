using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Interface.Bruto;
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
            
            repository.Add(bh);
            return bh;
        }

        public List<BrutoHonorar> GetAll()
        {
            return repository.GetAll();
        }

        public BrutoHonorar Get(BrutoHonorarDto dto)
        {
            return repository.Find(dto.Id);
        }

        

        public BrutoHonorar Edit(BrutoHonorarDto dto)
        {
            BrutoHonorar brutoHonorar = Get(dto);
            if (brutoHonorar != null)
            {
                brutoHonorar.TrenutnaPlata = dto.TrenutnaPlata;
                if (dto.valuta == "KM")
                {
                    brutoHonorar.valuta = Valuta.KM;
                }
                else if (dto.valuta == "EUR")
                {
                    brutoHonorar.valuta = Valuta.EUR;
                }
                else
                {
                    brutoHonorar.valuta = Valuta.RSD;
                }
                return repository.Edit(brutoHonorar);
            }
            else
            {
                brutoHonorar = new BrutoHonorar { TrenutnaPlata = dto.TrenutnaPlata };
                if (dto.valuta == "KM")
                {
                    brutoHonorar.valuta = Valuta.KM;
                }
                else if (dto.valuta == "EUR")
                {
                    brutoHonorar.valuta = Valuta.EUR;
                }
                else
                {
                    brutoHonorar.valuta = Valuta.RSD;
                }
                return repository.Add(brutoHonorar);
            }
        }

        public BrutoHonorar GetById(int id)
        {
            return repository.Find(id);
        }

        public BrutoHonorar Add(BrutoHonorar honorar)
        {
            return repository.Add(honorar);
        }
    }
}
