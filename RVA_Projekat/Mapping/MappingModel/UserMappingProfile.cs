using RVA_Projekat.Dto;
using RVA_Projekat.Model;
using AutoMapper;

namespace RVA_Projekat.Mapping.MappingModel
{
    public class UserMappingProfile : Profile
    {
        private static UserMappingProfile instance;
        private UserMappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
        public static UserMappingProfile GetInstance()
        {
            if(instance==null)
                instance = new UserMappingProfile();
            return instance;
        }
    }
}
