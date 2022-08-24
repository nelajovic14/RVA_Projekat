using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Interface.InterfaceUser
{
    public interface IUserService
    {
        string Login(UserDto dto);
        User DodajEntitet(UserRegisterDto dto);
        User Edit(UserEditdto dto);
        User Get(UserDto dto);
    }
}
