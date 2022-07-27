using RVA_Projekat.Enums;
using System.Collections.Generic;

namespace RVA_Projekat.Model
{
    public class NetoHonorar
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

        public int Id { get; set; }
        public int IdPorez { get; set; }
        public List<Porez> Porezi { get; set; }
        public Umanjenje umanjenje { get; set; }    
        public Uvecanje uvecanje { get; set; }
        public int BrutoHonorarId { get; set; }
        public BrutoHonorar honorar { get; set; }
    }
}
