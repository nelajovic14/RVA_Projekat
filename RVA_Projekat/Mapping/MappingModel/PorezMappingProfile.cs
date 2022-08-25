using AutoMapper;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Mapping.MappingModel
{
    public class PorezMappingProfile : Profile
    {
        private static PorezMappingProfile instance;
        private PorezMappingProfile()
        {
            CreateMap<Porez, PorezDto>().ReverseMap();
        }
        public static PorezMappingProfile GetInstance()
        {
             if(instance == null)
                instance = new PorezMappingProfile();

             return instance;
        }
    }
}
