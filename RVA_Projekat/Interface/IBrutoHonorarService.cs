using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Interface
{
    public interface IBrutoHonorarService
    {
        BrutoHonorar DodajEntitet(BrutoHonorarDto dto);
        List<BrutoHonorar> GetAll();
        BrutoHonorar GetById(int id);
        void Delete(BrutoHonorar brutoHonorar);
        BrutoHonorar GetByPlataIValuta(int plata,Valuta v);
    }
}
