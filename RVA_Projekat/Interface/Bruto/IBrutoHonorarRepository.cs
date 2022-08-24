using RVA_Projekat.Model;

namespace RVA_Projekat.Interface.Bruto
{
    public interface IBrutoHonorarRepository : IRepository<BrutoHonorar>
    {
        BrutoHonorar Edit(BrutoHonorar bh);
    }
}
