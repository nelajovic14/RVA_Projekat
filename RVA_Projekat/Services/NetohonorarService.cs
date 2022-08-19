using RVA_Projekat.Dto;
using RVA_Projekat.Interface;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Services
{
    public class NetohonorarService : INetohonorarService
    {
        private INetohonorarRepository netohonorarService;
        private IPorezRepository PorezRepository;
        private IBrutoHonorarService brutoHonorarService;

        public NetohonorarService(INetohonorarRepository netohonorarService, IBrutoHonorarService brutoHonorarService, IPorezRepository porezRepository)
        {
            this.netohonorarService = netohonorarService;
            this.brutoHonorarService = brutoHonorarService;
            PorezRepository = porezRepository;
        }


        public NetoHonorar DodajEntitet(NetoHonorarDto dto)
        {
            NetoHonorar nh = new NetoHonorar();
           
            if (dto.uvecanje == "PET")
            {
                nh.uvecanje = Enums.Uvecanje.PET;
            }
            else if (dto.uvecanje == "DESET")
            {
                nh.uvecanje = Enums.Uvecanje.DESET;
            }
            else if (dto.uvecanje == "DVADESET")
            {
                nh.uvecanje = Enums.Uvecanje.DVADEST;
            }
            else if (dto.uvecanje == "PEDESET")
            {
                nh.uvecanje = Enums.Uvecanje.PEDESET;
            }
            else if (dto.uvecanje == "SEDAMDESET")
            {
                nh.uvecanje = Enums.Uvecanje.SEDAMDESET;
            }
            else if (dto.uvecanje == "STO")
            {
                nh.uvecanje = Enums.Uvecanje.STO;
            }

            if (dto.umanjenje == "PET")
            {
                nh.umanjenje = Enums.Umanjenje.PET;
            }
            else if (dto.umanjenje == "DESET")
            {
                nh.umanjenje = Enums.Umanjenje.DESET;
            }
            else if (dto.umanjenje == "DVADESET")
            {
                nh.umanjenje = Enums.Umanjenje.DVADEST;
            }
            else if (dto.umanjenje == "PEDESET")
            {
                nh.umanjenje = Enums.Umanjenje.PEDESET;
            }
            else if (dto.umanjenje == "SEDAMDESET")
            {
                nh.umanjenje = Enums.Umanjenje.SEDAMDESET;
            }
            else if (dto.umanjenje == "STO")
            {
                nh.umanjenje = Enums.Umanjenje.STO;
            }
            nh.BrutoHonorarId = dto.BrutoHonorarId;
            //nh.honorar = brutoHonorarService.GetById(dto.BrutoHonorarId);
            netohonorarService.Add(nh);
            List<Porez> porezs = new List<Porez>();
            foreach (var p in dto.Porezi)
            {
                if (p == "POTROSNJA")
                    porezs.Add(new Porez (Enums.PorezType.POTROSNJA,nh.Id ));
                else if (p == "DOBIT")
                    porezs.Add(new Porez(Enums.PorezType.DOBIT, nh.Id ));
                else if (p == "DOHODAK")
                    porezs.Add(new Porez( Enums.PorezType.DOHODAK, nh.Id ));
                else if (p == "IMOVINA")
                    porezs.Add(new Porez ( Enums.PorezType.IMOVINA, nh.Id ));

            }
            nh.Porezi = porezs;
            foreach (Porez p in porezs)
            {
                PorezRepository.Add(p);
            }
            PorezRepository.SaveAll();
            return nh;
        }

        public NetoHonorar Get(int id)
        {
            NetoHonorar nh= netohonorarService.Find(id);
            if (nh == null)
                return null;
            List<Porez> porezs = PorezRepository.GetAll();
            foreach (var p in porezs)
            {
                if (p.NetoHonorarId == nh.Id)
                {
                    nh.Porezi.Add(p);
                }
            }
            return nh;
        }

        public List<NetoHonorar> GetAll()
        {
            List<NetoHonorar> netohonorars =netohonorarService.GetAll();
            List<Porez> porezs = PorezRepository.GetAll();

            return netohonorars;

        }

        public void Obrisi(NetoHonorar netoHonorar)
        {
            List<Porez> porezs= PorezRepository.GetAll();
            foreach(var p in porezs)
            {
                if (p.NetoHonorarId == netoHonorar.Id)
                    PorezRepository.Remove(p);
            }
            PorezRepository.SaveAll();
            netohonorarService.Remove(netoHonorar);
        }
    }
}
