using AutoMapper;
using Microsoft.Extensions.Configuration;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Entities;
using PentaGol.Domain.Entities.ImageEntities;
using PentaGol.Service.DTOs;
using PentaGol.Service.DTOs.Ligas;
using PentaGol.Service.Exceptions;
using PentaGol.Service.Extensions;
using PentaGol.Service.Helpers;
using PentaGol.Service.Interfaces;
using System;
using System.CodeDom;
using System.Data;

namespace PentaGol.Service.Services;

public class LigaService : ILigaService
{
    #region D.A Configuration
    private readonly IMapper mapper;
    private readonly IRepository<Liga> ligaRepository;
    private readonly IRepository<LigaImage> ligaImageRepository;
    /// <summary>
    /// Containing in D.A
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="ligaRepository"></param>
    /// <param name="ligaImageRepository"></param>
    public LigaService(IMapper mapper, IRepository<Liga> ligaRepository, IRepository<LigaImage> ligaImageRepository)
    {
        this.mapper = mapper;
        this.ligaRepository = ligaRepository;
        this.ligaImageRepository = ligaImageRepository;
    }
    #endregion

    #region Create Liga
    /// <summary>
    /// Create Method
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto)
    {
        Liga liga = await this.ligaRepository.SelectAsync(l => l.Name.ToLower() == dto.Name.ToLower());
        if (liga is not null)
            throw new PentaGolException(403, "Liga already exists with this name");

        Liga mappedLiga = mapper.Map<Liga>(dto);
        var result = await this.ligaRepository.InsertAsync(mappedLiga);
        await this.ligaRepository.SaveChangesAsync();
        return this.mapper.Map<LigaForResultDto>(result);

    }
    #endregion

    #region Update Liga
    /// <summary>
    /// Modify method to edit
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<LigaForResultDto> ModifyAsync(Liga dto)
    {
        var modifyingLiga = await this.ligaImageRepository.SelectAsync(l => l.Id.Equals(dto.Id));
        if (modifyingLiga is null)
            throw new PentaGolException(404, "Liga couldn't be found");

        this.mapper.Map(dto, modifyingLiga);
        modifyingLiga.UpdatedAt = DateTime.UtcNow;
        await this.ligaImageRepository.SaveChangesAsync();

        var result = mapper.Map<LigaForResultDto>(modifyingLiga);
        result.Image = mapper.Map<LigaImageForResultDto>(
            await this.ligaImageRepository.SelectAsync(l => l.LigaId.Equals(result.Id)));
        return result;
        
    }

    #endregion

    #region Remove Liga
    /// <summary>
    /// Removing liga
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveAsync(int id)
    {
        var liga = await this.ligaRepository.SelectAsync(l => l.Id.Equals(id));
        if (liga is null)
            throw new PentaGolException(404, "Liga couldn't be found");
        await this.ligaRepository.DeleteAsync(liga);
        await this.ligaRepository.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Remove Image
    /// <summary>
    /// Disposing an image
    /// </summary>
    /// <param name="ligaId"></param>
    /// <returns></returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<bool> RemoveImageAsync(int ligaId)
    {
        var ligaImage = await this.ligaImageRepository.SelectAsync(t => t.LigaId.Equals(ligaId));
        if (ligaImage is null)
            throw new PentaGolException(404, "Image is not found");

        File.Delete(ligaImage.Path);
        await this.ligaImageRepository.DeleteAsync(ligaImage);
        await this.ligaImageRepository.SaveChangesAsync();
        return true;
    }

    #endregion

    #region GetAll Ligas
    /// <summary>
    /// Fetching all Ligas
    /// </summary>
    /// <returns>IEnumerable</returns>
    public async Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync()
    {
        var ligas = ligaRepository.SelectAll(isTracking: false);
        var result = mapper.Map<IEnumerable<LigaForResultDto>>(ligas);

        foreach (var item in result)
            item.Image = mapper.Map<LigaImageForResultDto>(
                await this.ligaImageRepository.SelectAsync(l => l.LigaId.Equals(item.Id)));
        return result;
    }

    #endregion

    #region GetById Liga
    /// <summary>
    /// Fetching Ligas by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>One Liga</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<LigaForResultDto> RetrieveByIdAsync(int id)
    {
        var liga = await this.ligaRepository.SelectAsync(l => l.Id.Equals(id));
        if (liga is null)
            throw new PentaGolException(404, "Liga couldn't be found");

        var result = mapper.Map<LigaForResultDto>(liga);
        result.Image = mapper.Map<LigaImageForResultDto>(
            await this.ligaImageRepository.SelectAsync(l => l.Id.Equals(result.Id)));

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
    public async Task<LigaImageForResultDto> RetrieveImageAsync(int ligaId)
    {
        var ligaImage = await this.ligaImageRepository.SelectAsync(t => t.LigaId.Equals(ligaId));
        if (ligaImage is null)
            throw new PentaGolException(404, "Image is not found");
        return mapper.Map<LigaImageForResultDto>(ligaImage);
    }

    #endregion

    #region Upload Image
    /// <summary>
    /// Uploading Image from a source (e.g Laptop or Computer)
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Image</returns>
    /// <exception cref="PentaGolException"></exception>
    public async Task<LigaImageForResultDto> UploadImageAsync(LigaImageForCreationDto dto)
    {
        var liga = await this.ligaRepository.SelectAsync(t => t.Id.Equals(dto.LigaId));
        if (liga is null)
            throw new PentaGolException(404, "Liga is not found");

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

        var ligaImage = new LigaImage
        {
            Name = fileName,
            Path = fullPath,
            LigaId = dto.LigaId,
            Liga = liga,
            CreatedAt = DateTime.UtcNow,
        };

        var createdImage = await this.ligaImageRepository.InsertAsync(ligaImage);
        await this.ligaImageRepository.SaveChangesAsync();
        return mapper.Map<LigaImageForResultDto>(createdImage);
    }
}
#endregion