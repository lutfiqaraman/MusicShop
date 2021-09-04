using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.Validations;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService ArtistService;
        private readonly IMapper Mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            ArtistService = artistService;
            Mapper        = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistVM>>> GetAllArtists()
        {
            IEnumerable<Artist> artists = await ArtistService.GetAllArtists();
            IEnumerable<ArtistVM> lstArtist =
                Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistVM>>(artists);

            return Ok(lstArtist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistVM>> GetArtistById(int id)
        {
            Artist artist = await ArtistService.GetArtistById(id);

            if (artist == null)
                return NotFound("Music cannot be found");

            ArtistVM result = Mapper.Map<Artist, ArtistVM>(artist);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistVM>> CreateArtist([FromBody] SaveArtistVM model)
        {
            SaveArtistVMValidator musicValidator = new SaveArtistVMValidator();
            ValidationResult result = await musicValidator.ValidateAsync(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            Artist artist = Mapper.Map<SaveArtistVM, Artist>(model);
            Artist newArtist = await ArtistService.CreateArtist(artist);

            return Ok(newArtist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistVM>> UpdateArtist(int id, [FromBody] SaveArtistVM updateArtistVM)
        {
            SaveArtistVMValidator artistValidator = new SaveArtistVMValidator();
            ValidationResult result = await artistValidator.ValidateAsync(updateArtistVM);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            Artist artistToBeUpdated = await ArtistService.GetArtistById(id);

            if (artistToBeUpdated == null)
                return NotFound();

            Artist updatedArtist = Mapper.Map<SaveArtistVM, Artist>(updateArtistVM);
            await ArtistService.UpdateArtist(artistToBeUpdated, updatedArtist);

            Artist artistNewUpdate = await ArtistService.GetArtistById(id);
            ArtistVM artist = Mapper.Map<Artist, ArtistVM>(artistNewUpdate);

            return Ok(artist);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtistVM>> DeleteMusic(int id)
        {
            Artist artist = await ArtistService.GetArtistById(id);

            if (artist == null)
                NotFound();

            await ArtistService.DeleteArtist(artist);

            return NoContent();
        }
    }
}
