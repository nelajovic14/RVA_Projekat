using RVA_Projekat.Model;

namespace RVA_Projekat.Interface.InterfaceZaposlenih
{
    public interface IZaposleniRepository : IRepository<Zaposleni>
    {
        Zaposleni Edit(Zaposleni zaposleni);
    }
}
