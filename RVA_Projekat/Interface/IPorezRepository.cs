using RVA_Projekat.Model;

namespace RVA_Projekat.Interface
{
    public interface IPorezRepository:IRepository<Porez>
    {
        void SaveAll();
    }
}
