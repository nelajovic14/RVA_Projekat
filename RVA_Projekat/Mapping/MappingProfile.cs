using AutoMapper;
using RVA_Projekat.Dto;
using RVA_Projekat.Mapping.MappingModel;
using RVA_Projekat.Model;

namespace RVA_Projekat.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*CreateMap<Zaposleni, ZaposleniDto>().ReverseMap(); //Kazemo mu da mapira Subject na SubjectDto i obrnuto
            CreateMap<BrutoHonorar, BrutoHonorarDto>().ReverseMap();
            CreateMap<NetoHonorar, NetoHonorarDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<Porez, PorezDto>().ReverseMap();*/
            ZaposleniMappingProfile.GetInstance();
            BrutoHonorarMappingProfile.GetInstance();
            NetoHonorarMappingProfile.GetInstance();
            UserMappingProfile.GetInstance();
            PorezMappingProfile.GetInstance();
        }

    }
}
