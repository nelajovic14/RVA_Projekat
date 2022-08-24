using RVA_Projekat.Enums;
using RVA_Projekat.Interface;

namespace RVA_Projekat.Model
{
    public class BrutoHonorar:IDupliranje<BrutoHonorar>
    {
        public BrutoHonorar()
        {
        }

        public BrutoHonorar(int trenutnaPlata, Valuta valuta)
        {
            TrenutnaPlata = trenutnaPlata;
            this.valuta = valuta;
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

        public BrutoHonorar Dupliraj()
        {
            BrutoHonorar newBruto = new BrutoHonorar();
            newBruto.TrenutnaPlata = this.TrenutnaPlata;
            newBruto.valuta = this.valuta;

            return newBruto;

        }
    }
}
