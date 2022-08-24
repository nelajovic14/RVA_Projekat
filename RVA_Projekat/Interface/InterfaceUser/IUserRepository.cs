using RVA_Projekat.Model;
using System.Threading.Tasks;

namespace RVA_Projekat.Interface.InterfaceUser
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUsername(string username);
        User Edit(User user);
    }
}
