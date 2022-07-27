using RVA_Projekat.Enums;

namespace RVA_Projekat.Model
{
    public class BrutoHonorar
    {
        public BrutoHonorar()
        {
        }

        public BrutoHonorar(int id, int trenutnaPlata, Valuta valuta)
        {
            Id = id;
            TrenutnaPlata = trenutnaPlata;
            this.valuta = valuta;
        }

        public int Id { get; set; }
        public int TrenutnaPlata { get; set; }
        public Valuta valuta { get; set; }
    }
}
