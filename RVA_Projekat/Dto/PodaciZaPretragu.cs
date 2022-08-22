using System.Collections.Generic;

namespace RVA_Projekat.Dto
{
    public class PodaciZaPretragu
    {
        public PodaciZaPretragu()
        {
        }

        public PodaciZaPretragu(List<string> porezi, string uvecanje, string umanjenje, int plata, string valuta)
        {
            Porezi = porezi;
            Uvecanje = uvecanje;
            Umanjenje = umanjenje;
            Plata = plata;
            Valuta = valuta;
        }

        public List<string> Porezi { get; set; }
        public string Uvecanje { get; set; }
        public string Umanjenje { get; set; }
        public int Plata { get; set; }
        public string Valuta { get; set; }
        public string Korisnik { get; set; }
    }
}
