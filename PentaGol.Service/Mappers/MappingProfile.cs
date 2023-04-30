using AutoMapper;
using PentaGol.Domain.Entities;
using PentaGol.Domain.Entities.ImageEntities;
using PentaGol.Service.DTOs.Games;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.News;
using PentaGol.Service.DTOs.Teams;

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
        CreateMap<TeamImage, TeamForResultDto>().ReverseMap();

        //Mapping Liga entity 
        CreateMap<Liga, LigaForCreationDto>().ReverseMap();
        CreateMap<Liga, LigaForResultDto>().ReverseMap();
        CreateMap<LigaImage, LigaImageForResultDto>().ReverseMap();

    }
}
