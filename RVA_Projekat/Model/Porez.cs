using RVA_Projekat.Enums;
using RVA_Projekat.Interface;

namespace RVA_Projekat.Model
{
    public class Porez:IDupliranje<Porez>
    {
        public Porez()
        {
        }

        public Porez(PorezType tip, int netoHonorarId)
        {
            Tip = tip;
            NetoHonorarId = netoHonorarId;
        }

        public Porez(int id, PorezType tip, int netoHonorarId)
        {
            Id = id;
            Tip = tip;
            NetoHonorarId = netoHonorarId;
        }

        public int Id { get; set; } 
        public PorezType Tip { get; set; }
        public int NetoHonorarId { get; set; }

        public Porez Dupliraj()
        {
            Porez newPorez = new Porez();
            newPorez.Tip = this.Tip;
            return newPorez;
        }
    }
}
