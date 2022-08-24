using RVA_Projekat.Infrastructure;
using RVA_Projekat.Interface.Bruto;
using RVA_Projekat.Interface.Neto;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Initilaizer
{
    public class NetohonorarInitializer : INetohonorarInitializer
    {
        INetohonorarRepository repository;
        IBrutoHonorarRepository honorarRepository;

        public NetohonorarInitializer(INetohonorarRepository repository, IBrutoHonorarRepository brutoHonorarRepository)
        {
            this.repository = repository;
            this.honorarRepository = brutoHonorarRepository;
        }

        public void InitializeNetohonorars()
        {
            List<NetoHonorar> netoHonorars = repository.GetAll();
            NetoHonorar nh = new NetoHonorar{ umanjenje = Enums.Umanjenje.DESET, uvecanje = Enums.Uvecanje.DVADESET};
            NetoHonorar nh2 = new NetoHonorar{  umanjenje = Enums.Umanjenje.PET, uvecanje = Enums.Uvecanje.DVADESET };
            NetoHonorar nh3 = new NetoHonorar{  umanjenje = Enums.Umanjenje.DESET, uvecanje = Enums.Uvecanje.DESET };
            if (netoHonorars.Count > 3)
            {
                    return;
                
            }
            foreach(BrutoHonorar bh in honorarRepository.GetAll())
            {
                nh.BrutoHonorarId = bh.Id;
                nh2.BrutoHonorarId = bh.Id;
                nh3.BrutoHonorarId = bh.Id;
                break;
            }
            repository.Add(nh);
            repository.Add(nh2);
            repository.Add(nh3);
        }
    }
}
