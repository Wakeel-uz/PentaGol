using AutoMapper;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs;

namespace PentaGol.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Mapping Game entity
        CreateMap<Game, GameForCreationDto>().ReverseMap();
        CreateMap<Game, GameForResultDto>().ReverseMap();

        //Mapping News entity
        CreateMap<News, NewsForCreationDto>().ReverseMap();

        //Mapping Team entity
        CreateMap<Team, TeamForCreationDto>().ReverseMap();
        CreateMap<Team, TeamForResultDto>().ReverseMap();

        //Mapping Liga entity 
        CreateMap<Liga, LigaForCreationDto>().ReverseMap();
        CreateMap<Liga, LigaForResultDto>().ReverseMap();
    }
}
