using RVA_Projekat.Enums;
using RVA_Projekat.Interface;
using System;
using System.Collections.Generic;

namespace RVA_Projekat.Model
{
    public class NetoHonorar:IDupliranje<NetoHonorar>
    {
        public NetoHonorar()
        {
        }

        public NetoHonorar(int id, List<Porez> porezi, Umanjenje umanjenje, Uvecanje uvecanje)
        {
            Id = id;
            Porezi = porezi;
            this.umanjenje = umanjenje;
            this.uvecanje = uvecanje;
        }
        public NetoHonorar( Umanjenje umanjenje, Uvecanje uvecanje,int brutoHonorarId)
        {
            
            this.umanjenje = umanjenje;
            this.uvecanje = uvecanje;
            this.BrutoHonorarId=brutoHonorarId;
        }
        public NetoHonorar(int id,  List<Porez> porezi, Umanjenje umanjenje, Uvecanje uvecanje, int brutoHonorarId, BrutoHonorar honorar)
        {
            Id = id;
            Porezi = porezi;
            this.umanjenje = umanjenje;
            this.uvecanje = uvecanje;
            BrutoHonorarId = brutoHonorarId;
            this.honorar = honorar;
        }

        public int Id { get; set; }
        public List<Porez> Porezi { get; set; }
        public Umanjenje umanjenje { get; set; }    
        public Uvecanje uvecanje { get; set; }
        public int BrutoHonorarId { get; set; }
        public BrutoHonorar honorar { get; set; }
        public DateTime new_date { get; set; }
        public NetoHonorar Dupliraj()
        {
            NetoHonorar newNeto = new NetoHonorar();
            newNeto.umanjenje = this.umanjenje;
            newNeto.uvecanje = this.uvecanje;
            newNeto.Porezi = new List<Porez>();
            if (this.Porezi != null)
            {
                foreach(var p in this.Porezi)
                {
                    newNeto.Porezi.Add(p.Dupliraj());
                }
            }
            if (this.honorar != null)
            {
                newNeto.honorar=this.honorar.Dupliraj();
            }
            return newNeto;
        }
    }
}
