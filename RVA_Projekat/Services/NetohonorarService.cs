using RVA_Projekat.Dto;
using RVA_Projekat.Interface;
using RVA_Projekat.Interface.Bruto;
using RVA_Projekat.Interface.Neto;
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

        public NetoHonorar Dodaj(NetoHonorar neto)
        {
            return netohonorarService.Add(neto);
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
                nh.uvecanje = Enums.Uvecanje.DVADESET;
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
                nh.umanjenje = Enums.Umanjenje.DVADESET;
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
            if (brutoHonorarService.GetById(dto.BrutoHonorarId) != null)
                nh.honorar = brutoHonorarService.GetById(dto.BrutoHonorarId);
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

        public NetoHonorar Dupliraj(NetoHonorarDto honorar)
        {
            NetoHonorar nh = netohonorarService.Find(honorar.Id);
            BrutoHonorar bruto = brutoHonorarService.GetById(honorar.BrutoHonorarId);
            nh.honorar = bruto;
            /*List<Porez> porezs = new List<Porez>();
            if (honorar.Porezi != null)
            {
                foreach (var p in honorar.Porezi)
                {
                    if (p == "POTROSNJA")
                        porezs.Add(new Porez(Enums.PorezType.POTROSNJA, nh.Id));
                    else if (p == "DOBIT")
                        porezs.Add(new Porez(Enums.PorezType.DOBIT, nh.Id));
                    else if (p == "DOHODAK")
                        porezs.Add(new Porez(Enums.PorezType.DOHODAK, nh.Id));
                    else if (p == "IMOVINA")
                        porezs.Add(new Porez(Enums.PorezType.IMOVINA, nh.Id));

                }
            }
            nh.Porezi= porezs;*/
            List<Porez> porezs = PorezRepository.GetAll();
            NetoHonorar newNeto = nh.Dupliraj();
            BrutoHonorar newBruto= brutoHonorarService.Add(newNeto.honorar);
            newNeto.BrutoHonorarId = newBruto.Id;
            netohonorarService.Add(newNeto);
            foreach(var p in newNeto.Porezi)
            {
                p.NetoHonorarId = newNeto.Id;
                PorezRepository.Add(p);
            }
            return newNeto;
            /*NetoHonorar nh = netohonorarService.Find(honorar.Id);
            NetoHonorar newNeto=new NetoHonorar { umanjenje=nh.umanjenje,uvecanje=nh.uvecanje };
            BrutoHonorar bruto= brutoHonorarService.GetById(honorar.BrutoHonorarId);
            BrutoHonorar bh= new BrutoHonorar { TrenutnaPlata = bruto.TrenutnaPlata, valuta = bruto.valuta };
            BrutoHonorar newBruto= brutoHonorarService.Add(bh);
            newNeto.BrutoHonorarId = newBruto.Id;
            newNeto.honorar = newBruto;
            
            
            NetoHonorar neto= netohonorarService.Add(newNeto);
            List<Porez> porezs = new List<Porez>();
            if (honorar.Porezi != null)
            {
                foreach (var p in honorar.Porezi)
                {

                    
                    if (p == "POTROSNJA")
                        porezs.Add(new Porez(Enums.PorezType.POTROSNJA, neto.Id));
                    else if (p == "DOBIT")
                        porezs.Add(new Porez(Enums.PorezType.DOBIT, neto.Id));
                    else if (p == "DOHODAK")
                        porezs.Add(new Porez(Enums.PorezType.DOHODAK, neto.Id));
                    else if (p == "IMOVINA")
                        porezs.Add(new Porez(Enums.PorezType.IMOVINA, neto.Id));

                }
            }
            foreach(var p in porezs)
            {
                PorezRepository.Add(p);
            }
            PorezRepository.SaveAll();
            neto.Porezi = porezs;
            return neto;*/
        }

        public NetoHonorar Edit(NetoHonorarDto dto)
        {
            NetoHonorar nh = netohonorarService.Find(dto.Id);
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
                nh.uvecanje = Enums.Uvecanje.DVADESET;
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
                nh.umanjenje = Enums.Umanjenje.DVADESET;
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
            List<Porez> porezs1 = PorezRepository.GetAll();
            foreach(var p in porezs1)
            {
                if (p.NetoHonorarId == nh.Id)
                    PorezRepository.Remove(p);
            }
            List<Porez> porezs = new List<Porez>();
            foreach (var p in dto.Porezi)
            {
                if (p == "POTROSNJA")
                    porezs.Add(new Porez(Enums.PorezType.POTROSNJA, nh.Id));
                else if (p == "DOBIT")
                    porezs.Add(new Porez(Enums.PorezType.DOBIT, nh.Id));
                else if (p == "DOHODAK")
                    porezs.Add(new Porez(Enums.PorezType.DOHODAK, nh.Id));
                else if (p == "IMOVINA")
                    porezs.Add(new Porez(Enums.PorezType.IMOVINA, nh.Id));

            }
            
            foreach (Porez p in porezs)
            {
                PorezRepository.Add(p);
            }
            PorezRepository.SaveAll();

            NetoHonorar editovan= netohonorarService.Edit(nh);
            return editovan;
        }

        public NetoHonorar Get(NetoHonorarDto dto)
        {
            NetoHonorar nh= netohonorarService.Find(dto.Id);
            if (nh == null)
                return null;
            nh.Porezi = new List<Porez>();
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
            if (netoHonorar != null)
            {
                List<Porez> porezs = PorezRepository.GetAll();
                foreach (var p in porezs)
                {
                    if (p.NetoHonorarId == netoHonorar.Id)
                        PorezRepository.Remove(p);
                }
                PorezRepository.SaveAll();
                netohonorarService.Remove(netoHonorar);
            }
        }


    }
}
