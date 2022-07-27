using RVA_Projekat.Enums;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Dto
{
    public class NetoHonorarDto
    {
        public int Id { get; set; }
        public List<Porez> Porezi { get; set; }
        public Umanjenje umanjenje { get; set; }
        public Uvecanje uvecanje { get; set; }
        public BrutoHonorar honorar { get; set; }
    }
}
