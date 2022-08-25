using AutoMapper;
using RVA_Projekat.Dto;
using RVA_Projekat.Model;

namespace RVA_Projekat.Mapping.MappingModel
{
    public class BrutoHonorarMappingProfile:Profile
    {
        private static BrutoHonorarMappingProfile instance;
        private BrutoHonorarMappingProfile()
        {
            CreateMap<BrutoHonorar, BrutoHonorarDto>().ReverseMap();
        }
        public static BrutoHonorarMappingProfile GetInstance()
        {
            if (instance == null)
                instance = new BrutoHonorarMappingProfile();

            return instance;
        }
    }
}
