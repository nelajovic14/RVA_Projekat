using RVA_Projekat.Model;
using System.Threading.Tasks;

namespace RVA_Projekat.Interface
{
    public interface IUserRepository:IRepository<User>
    {
         User FindByUsername(string username);
    }
}
