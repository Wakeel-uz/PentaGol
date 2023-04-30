using AutoMapper;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Entities;
using PentaGol.Service.DTOs.Games;
using PentaGol.Service.Exceptions;
using PentaGol.Service.Interfaces;

namespace PentaGol.Service.Services;

#pragma warning disable
public class GameService : IGameService
{
    #region D.A Configuration
    private readonly IMapper _mapper;
    private readonly IRepository<Game> _gameRepository;
    private readonly ITeamService _teamService;
    /// <summary>
    /// Containing in D.A
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="gameRepository"></param>
    public GameService(
        IMapper mapper,
        IRepository<Game> gameRepository
,
        ITeamService teamService)
    {
        this._mapper = mapper;
        this._gameRepository = gameRepository;
        _teamService = teamService;
    }
    #endregion

    #region Create Game
    /// <summary>
    /// Create Method
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<GameForResultDto> CreateAsync(DTOs.Games.GameForCreationDto dto)
    {
        var mappedGame = _mapper.Map<Game>(dto);
        if(mappedGame.IsFinished == true)
        {
            var firstTeam = _teamService.RetrieveById(mappedGame.FirstTeamId);
            var secondTeam = _teamService.RetrieveById(mappedGame.SecondTeamId);
            var mappedFirstTeam = _mapper.Map<Team>(firstTeam);
            var mappedSecondTeam = _mapper.Map<Team>(secondTeam);
            if (mappedGame.FirstTeamScore > mappedGame.SecondTeamScore)
            {
                //First team wins
                mappedFirstTeam.TotalScore += 3;
                mappedFirstTeam.TotalGame += 1;
                mappedFirstTeam.TotalScoredGoals += mappedGame.FirstTeamScore;
                mappedFirstTeam.TotalReceivedGoals += mappedGame.SecondTeamScore;

                mappedSecondTeam.TotalGame += 1;
                mappedSecondTeam.TotalScoredGoals = mappedGame.SecondTeamScore;
                mappedSecondTeam.TotalReceivedGoals = mappedGame.FirstTeamScore;
            }
            else if(mappedGame.SecondTeamScore > mappedGame.FirstTeamScore)
            {
                //Second team wins
                mappedSecondTeam.TotalScore += 3;
                mappedSecondTeam.TotalGame += 1;
                mappedSecondTeam.TotalScoredGoals += mappedGame.SecondTeamScore;
                mappedSecondTeam.TotalReceivedGoals += mappedGame.FirstTeamScore;

                mappedFirstTeam.TotalGame += 1;
                mappedFirstTeam.TotalScoredGoals += mappedGame.FirstTeamScore;
                mappedFirstTeam.TotalReceivedGoals += mappedGame.SecondTeamScore;
            }
            else
            {
                mappedFirstTeam.TotalScore += 1;
                mappedSecondTeam.TotalScore += 1;

                mappedFirstTeam.TotalGame += 1;
                mappedFirstTeam.TotalScoredGoals += mappedGame.FirstTeamScore;
                mappedFirstTeam.TotalReceivedGoals += mappedGame.SecondTeamScore;


                mappedSecondTeam.TotalGame += 1;
                mappedSecondTeam.TotalScoredGoals += mappedGame.SecondTeamScore;
                mappedSecondTeam.TotalReceivedGoals += mappedGame.FirstTeamScore;
            }
            await _teamService.ModifyAsync(mappedFirstTeam);
            await _teamService.ModifyAsync(mappedSecondTeam);
        }
        var result = await this._gameRepository.InsertAsync(mappedGame);
        await this._gameRepository.SaveChangesAsync();
        return this._mapper.Map<GameForResultDto>(result);

    }

    #endregion

    #region Update Game
    /// <summary>
    /// Modify method to edit
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<GameForResultDto> ModifyAsync(Game dto)
    {
        Game modifyingGame = await this._gameRepository.SelectAsync(p => p.Id.Equals(dto.Id));
        if (modifyingGame is null)
            throw new PentaGolException(404, "Game not found");

        this._mapper.Map(dto, modifyingGame);
        modifyingGame.UpdatedAt = DateTime.UtcNow;
        await this._gameRepository.SaveChangesAsync();
        return _mapper.Map<GameForResultDto>(modifyingGame);

    }

    #endregion

    #region Remove Game
    /// <summary>
    /// Removing game
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveAsync(int id)
    {
        var position = await this._gameRepository.SelectAsync(position => position.Id.Equals(id));
        if (position is null)
            throw new PentaGolException(404, "Game not found");

        await this._gameRepository.DeleteAsync(position);
        await this._gameRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GameForResultDto>> RetrieveAllAsync()
    {
        var games = this._gameRepository.SelectAll();

        if (games is null)
            throw new PentaGolException(404, "Product not found");

        var Result = this._mapper.Map<IEnumerable<Game>>(games);

        return (IEnumerable<GameForResultDto>)games;
    }

    public async Task<GameForResultDto> RetrieveByIdAsync(int id)
    {
        // Find merchant by id
        var game = await this._gameRepository.SelectAsync(m => m.Id == id);
        if (game == null)
            throw new PentaGolException(404, "Not found");

        // Map Entity to DTO and return
        return this._mapper.Map<GameForResultDto>(game);
    }

    
}
    #endregion