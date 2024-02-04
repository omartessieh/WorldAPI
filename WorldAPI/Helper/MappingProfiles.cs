using AutoMapper;
using Models.Dtos;
using WorldAPI.Entities;

namespace WorldAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Source, Destination
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<States, StatesDto>().ReverseMap();
            CreateMap<States, CreateStatesDto>().ReverseMap();
            CreateMap<States, UpdateStatesDto>().ReverseMap();
        }
    }
}