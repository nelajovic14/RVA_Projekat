using AutoMapper;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Mapping.MappingModel
{
    public class NetoHonorarMappingProfile:Profile
    {
        private static NetoHonorarMappingProfile instance;
        private NetoHonorarMappingProfile()
        {
            CreateMap<NetoHonorar, NetoHonorarDto>().ReverseMap();
        }
        public static NetoHonorarMappingProfile GetInstance()
        {
            if (instance == null)
                instance = new NetoHonorarMappingProfile();
            return instance;
        }
    }
}
