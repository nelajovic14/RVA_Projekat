using RVA_Projekat.Dto;
using RVA_Projekat.Enums;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Interface.Bruto
{
    public interface IBrutoHonorarService
    {
        BrutoHonorar DodajEntitet(BrutoHonorarDto dto);
        BrutoHonorar Add(BrutoHonorar honorar);
        List<BrutoHonorar> GetAll();
        BrutoHonorar Get(BrutoHonorarDto dto);
        BrutoHonorar GetById(int id);
        void Delete(BrutoHonorar brutoHonorar);
        BrutoHonorar Edit(BrutoHonorarDto dto);
    }
}
