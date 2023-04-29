using AutoMapper;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Games;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

public class GameService : IGameService
{
    private readonly IRepository<Game> _gameRepository;
    private readonly IMapper _mapper;

    public GameService(
        IRepository<Game> gameRepository, 
        IMapper mapper)
    {
        this._gameRepository = gameRepository;
        this._mapper = mapper;
    }

    public Task<GameForResultDto> CreateAsync(GameForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<GameForResultDto> ModifyAsync(GameForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GameForResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GameForResultDto> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
