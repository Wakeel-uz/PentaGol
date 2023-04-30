using AutoMapper;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Entities;
using PentaGol.Domain.Entities.ImageEntities;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.DTOs.Teams;
using PentaGol.Service.Exceptions;
using PentaGol.Service.Extensions;
using PentaGol.Service.Helpers;
using PentaGol.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentaGol.Service.Services;

public class TeamService : ITeamService
{
    #region D.A Configuration
    private readonly IMapper mapper;
    private readonly IRepository<Team> teamRepository;
    private readonly IRepository<TeamImage> teamImageRepository;

    /// <summary>
    /// D.A configuring
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="teamRepository"></param>
    /// <param name="teamImageRepository"></param>
    
    public TeamService(IMapper mapper, IRepository<Team> teamRepository, IRepository<TeamImage> teamImageRepository)
    {
        this.mapper = mapper;
        this.teamRepository = teamRepository;
        this.teamImageRepository = teamImageRepository;
    }
    #endregion

    #region Create Method
    
    /// <summary>
    /// Create Async Method
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Team</returns>
    /// <exception cref="PentaGolException"></exception> 
    public async Task<TeamForResultDto> CreateAsync(TeamForCreationDto dto)
    {
        Team team = await this.teamRepository.SelectAsync(l => l.Name.ToLower() == dto.Name.ToLower());
        if (team is not null)
            throw new PentaGolException(403, "Team already exists with this name");

        Team mappedTeam = mapper.Map<Team>(dto);
        var result = await this.teamRepository.InsertAsync(mappedTeam);
        await this.teamRepository.SaveChangesAsync();
        return this.mapper.Map<TeamForResultDto>(result);

    }
    #endregion

    #region Update Team
    /// <summary>
    /// Modify method to edit
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<TeamForResultDto> ModifyAsync(Team dto)
    {
        var modifyingTeam = await this.teamImageRepository.SelectAsync(t =>t.Id.Equals(dto.Id));
        if (modifyingTeam is null)
            throw new PentaGolException(404, "Team couldn't be found");

        this.mapper.Map(dto, modifyingTeam);
        modifyingTeam.UpdatedAt = DateTime.UtcNow;
        await this.teamImageRepository.SaveChangesAsync();

        var result = mapper.Map<TeamForResultDto>(modifyingTeam);
        result.Image = mapper.Map<TeamImageForResultDto>(
            await this.teamImageRepository.SelectAsync(t => t.TeamId.Equals(result.Id)));
        return result;

    }



    #endregion

    #region Remove Team
    /// <summary>
    /// Removing team
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveAsync(int id)
    {
        var team = await this.teamRepository.SelectAsync(t => t.Id.Equals(id));
        if (team is null)
            throw new PentaGolException(404, "Team couldn't be found");
        await this.teamRepository.DeleteAsync(team);
        await this.teamRepository.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Remove Image
    /// <summary>
    /// Disposing an image
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns>bool</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveImageAsync(int teamId)
    {
        var teamImage = await this.teamImageRepository.SelectAsync(t => t.TeamId.Equals(teamId));
        if (teamImage is null)
            throw new PentaGolException(404, "Team is not found");

        File.Delete(teamImage.Path);
        await this.teamImageRepository.DeleteAsync(teamImage);
        await this.teamImageRepository.SaveChangesAsync();
        return true;
    }

    #endregion

    #region GetAll Team
    /// <summary>
    /// Fetching all Teams
    /// </summary>
    /// <returns>IEnumerable</returns>
    public async Task<IEnumerable<TeamForResultDto>> RetrieveAllAsync()
    {
        var teams = teamRepository.SelectAll(isTracking: false);
        var result = mapper.Map<IEnumerable<TeamForResultDto>>(teams);

        foreach (var item in result)
            item.Image = mapper.Map<TeamImageForResultDto>(
                await this.teamImageRepository.SelectAsync(l => l.TeamId.Equals(item.Id)));
        return result;
    }

    public Task<TeamForResultDto> RetrieveById(int id)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region GetById Teams
    /// <summary>
    /// Fetching Teams by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>One Liga</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<TeamForResultDto> RetrieveByIdAsync(int id)
    {
        var team = await this.teamRepository.SelectAsync(l => l.Id.Equals(id));
        if (team is null)
            throw new PentaGolException(404, "Liga couldn't be found");

        var result = mapper.Map<TeamForResultDto>(team);
        result.Image = mapper.Map<TeamImageForResultDto>(
            await this.teamImageRepository.SelectAsync(t => t.Id.Equals(result.Id)));

        return result;
    }

    #endregion

    #region Get Image
    /// <summary>
    /// Fetching Image
    /// </summary>
    /// <param name="ligaId"></param>
    /// <returns>One Image</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<TeamImageForResultDto> RetrieveImageAsync(int teamId)
    {
        var teamImage = await this.teamImageRepository.SelectAsync(t => t.TeamId.Equals(teamId));
        if (teamImage is null)
            throw new PentaGolException(404, "Image is not found");
        return mapper.Map<TeamImageForResultDto>(teamImage);
    }

    #endregion

    #region Upload Image
    /// <summary>
    /// Uploading Image from a source (e.g Laptop or Computer)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Image</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<TeamImageForResultDto> UploadImageAsync(TeamImageForCreationDto dto)
    {
        var team = await this.teamRepository.SelectAsync(t => t.Id.Equals(dto.TeamId));
        if (team is null)
            throw new PentaGolException(404, "Team is not found");

        byte[] image = dto.Image.ToByteArray();
        var fileExtension = Path.GetExtension(dto.Image.FileName);
        var fileName = Guid.NewGuid().ToString("N") + fileExtension;
        var webRootPath = EnvironmentHelper.WebHostPath;
        var folder = Path.Combine(webRootPath, "uploads", "images");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, fileName);
        using var imageStream = new MemoryStream(image);

        using var imagePath = new FileStream(fullPath, FileMode.CreateNew);
        imageStream.WriteTo(imagePath);

        var teamImage = new TeamImage
        {
            Name = fileName,
            Path = fullPath,
            TeamId = dto.TeamId,
            Team = team,
            CreatedAt = DateTime.UtcNow,
        };
        
        var createdImage = await this.teamImageRepository.InsertAsync(teamImage);
        await this.teamImageRepository.SaveChangesAsync();
        return mapper.Map<TeamImageForResultDto>(createdImage);
    }
}
#endregion
