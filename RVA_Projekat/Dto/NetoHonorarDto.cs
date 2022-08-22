using RVA_Projekat.Enums;
using RVA_Projekat.Model;
using System.Collections.Generic;

namespace RVA_Projekat.Dto
{
    public class NetoHonorarDto
    {
        public int Id { get; set; }
        public int BrutoHonorarId { get; set; }
        public List<string> Porezi { get; set; }
        public string umanjenje { get; set; }
        public string uvecanje { get; set; }
        public string Korisnik { get; set; }
    }
}
