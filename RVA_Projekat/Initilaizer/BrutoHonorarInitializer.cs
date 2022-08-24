using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface.Bruto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Initilaizer
{
    public class BrutoHonorarInitializer : IBrutoHonorarInitializer
    {
        IBrutoHonorarRepository repository;
        public BrutoHonorarInitializer(IBrutoHonorarRepository repository)
        {
            this.repository = repository;
        }
    
        public void InitializBrutoHonorars()
        {
            List<BrutoHonorar> brutoHonorars = repository.GetAll();
            BrutoHonorar bh = new BrutoHonorar(180000, Enums.Valuta.RSD);
            BrutoHonorar bh2 = new BrutoHonorar(80000, Enums.Valuta.RSD);
            BrutoHonorar bh3 = new BrutoHonorar(120000, Enums.Valuta.RSD);
            if (brutoHonorars.Count > 3)
            {
                return;
            }
            repository.Add(bh);
            repository.Add(bh2);
            repository.Add(bh3);
        }
    }
}
