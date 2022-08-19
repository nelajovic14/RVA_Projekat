using RVA_Projekat.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RVA_Projekat.Interface
{
    public interface IRepository<T>
    {
        T Add(T entity);
        List<T> GetAll();
        T Find(int id);
        void Remove(T entity);
    }
}
