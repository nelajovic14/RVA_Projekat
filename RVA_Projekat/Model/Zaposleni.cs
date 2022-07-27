namespace RVA_Projekat.Model
{
    public class Zaposleni
    {
        public int GodineIskustva { get; set; }
        public int Id { get; set; }
        public string Ime { get;set; }
        public int BrutoHonorarId { get; set; }
        public BrutoHonorar honorar { get; set; }

        public Zaposleni(int godineIskustva, int id, string ime)
        {
            GodineIskustva = godineIskustva;
            Id = id;
            Ime = ime;
        }       
        public Zaposleni() { }
    }
}
