using AutoMapper;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Mapping.MappingModel
{
    public class ZaposleniMappingProfile : Profile
    {
        private static ZaposleniMappingProfile instance;
        private ZaposleniMappingProfile()
        {
            CreateMap<Zaposleni, ZaposleniDto>().ReverseMap();
        }
        public static ZaposleniMappingProfile GetInstance()
        {
            if (instance == null)
                instance = new ZaposleniMappingProfile();

            return instance;
        }
    }
}
