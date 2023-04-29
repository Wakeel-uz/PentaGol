using PentaGol.Service.DTOs;
using PentaGol.Service.Interfaces;
using System;
using System.Data;

namespace PentaGol.Service.Services;

public class LigaService : ILigaService
{
    private readonly IMapper mapper;
    private readonly IRepository<Liga> ligaRepository;
    private readonly IImageService imageService;

    public LigaService(IMapper mapper, IRepository<Liga> ligaRepository, IImageService imageService)
    {
        this.mapper = mapper;
        this.ligaRepository = ligaRepository;
        this.imageService = imageService;
    }

    public async Task<LigaForResultDto> CreateAsync(LigaForCreationDto dto)
    {
        // Check if the Liga already exists
        Liga existingLiga = await this.ligaRepository.SelectAsync(u => u.Name.ToLower() == dto.Name.ToLower());
        if (existingLiga is not null)
        {
            throw new PentaGolException(404, "Liga already exists with this name");
        }

        // Upload the image and get the file path
        string imagePath = null;
        if (dto.Image != null && dto.Image.Length > 0)
        {
            byte[] imageBytes = dto.Image.ToByteArray();
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
            imagePath = await this.imageService.SaveImageAsync(imageBytes, fileName);
        }

        // Map the LigaForCreationDto to Liga object
        Liga mappedLiga = mapper.Map<Liga>(dto);

        // Set the LogoPath to the image file path
        mappedLiga.LogoPath = imagePath;

        // Insert the Liga object to the database
        Liga result = await this.ligaRepository.InsertAsync(mappedLiga);
        await this.ligaRepository.SaveChangesAsync();

        // Map the result to LigaForResultDto and return
        return this.mapper.Map<LigaForResultDto>(result);
    }


    public async Task<LigaForResultDto> ModifyAsync(LigaForCreationDto dto)
    {
        var modifyingLiga = await this.ligaRepository.SelectAsync(u => u.Name.Equals(dto.Name));
        if (modifyingLiga is null)
            throw new PentaGolException(404, "Liga couldn't be found");

        // Update Liga properties
        this.mapper.Map(dto, modifyingLiga);
        modifyingLiga.UpdatedAt = DateTime.UtcNow;

        // Check if the image is not null and save it
        if (dto.Image != null)
        {
            // Generate a unique filename for the image
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);

            // Save the image using the IImageService
            var filePath = await this.imageService.SaveImageAsync(dto.Image.ToByteArray(), fileName);

            // Update the Liga entity with the path of the saved image
            modifyingLiga.LogoPath = filePath;
        }

        await this.ligaRepository.SaveChangesAsync();
        return this.mapper.Map<LigaForResultDto>(modifyingLiga);
    }


    public Task<bool> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LigaForResultDto>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LigaForResultDto> RetrieveByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
