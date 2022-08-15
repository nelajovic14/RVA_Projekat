using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Interface
{
    public interface IUserService
    {
        string Login(UserDto dto);
        bool Register(UserRegisterDto dto);
        void Edit(UserEditdto dto);
        User Get(UserDto dto);
    }
}
