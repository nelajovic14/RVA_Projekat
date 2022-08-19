using RVA_Projekat.Enums;

namespace RVA_Projekat.Model
{
    public class Porez
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

    }
}
