using RVA_Projekat.Model;

namespace RVA_Projekat.Interface.Neto
{
    public interface INetohonorarRepository : IRepository<NetoHonorar>
    {
        NetoHonorar Edit(NetoHonorar nh);
    }
}
