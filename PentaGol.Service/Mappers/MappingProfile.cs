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
        CreateMap<Game, DTOs.Games.GameForCreationDto>().ReverseMap();
        CreateMap<Game, GameForResultDto>().ReverseMap();

        //Mapping News entity
        CreateMap<News, NewsForCreationDto>().ReverseMap();
        CreateMap<NewsImage, NewsImageForResultDto>().ReverseMap();

        //Mapping Team entity
        CreateMap<Team, TeamForCreationDto>().ReverseMap();
        CreateMap<Team, TeamForResultDto>().ReverseMap();
        CreateMap<TeamImage, TeamForResultDto>().ReverseMap();

        //Mapping Liga entity 
        CreateMap<Liga, DTOs.Ligas.GameForCreationDto>().ReverseMap();
        CreateMap<Liga, LigaForResultDto>().ReverseMap();
        CreateMap<LigaImage, LigaImageForResultDto>().ReverseMap();

    }
}
