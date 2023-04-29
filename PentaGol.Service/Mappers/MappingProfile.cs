using AutoMapper;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs;

namespace PentaGol.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Game, GameForCreationDto>().ReverseMap();
        CreateMap<Game, GameForResultDto>().ReverseMap();

        CreateMap<News, NewsForCreationDto>().ReverseMap();
    }
}
