using RVA_Projekat.Model;

namespace RVA_Projekat.Dto
{
    public class ZaposleniDto
    {
        public int GodineIskustva { get; set; }
        public int Id { get; set; }
        public string Ime { get; set; }
        public int BrutoHonorarId { get; set; }
        public BrutoHonorar honorar { get; set; }
    }
}
