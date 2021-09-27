using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
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
    public class MusicController : ControllerBase
    {
        private readonly IMusicService MusicService;
        private readonly IMapper Mapper;

        public MusicController(IMusicService musicService, IMapper mapper)
        {
            MusicService = musicService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicVM>>> GetAllMusics()
        {
            IEnumerable<Music> musics = await MusicService.GetAllWithArtists();
            IEnumerable<MusicVM> lstMusic =
                Mapper.Map<IEnumerable<Music>, IEnumerable<MusicVM>>(musics);

            return Ok(lstMusic);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MusicVM>> GetMusicById(int id)
        {
            Music music = await MusicService.GetMusicById(id);

            if (music == null)
                return NotFound("Music cannot be found");
            
            MusicVM result = Mapper.Map<Music, MusicVM>(music);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<MusicVM>> CreateMusic([FromBody] SaveMusicVM model)
        {
            SaveMusicVMValidator musicValidator = new SaveMusicVMValidator();
            ValidationResult result = await musicValidator.ValidateAsync(model);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            Music music    = Mapper.Map<SaveMusicVM, Music>(model);
            Music newMusic = await MusicService.CreateMusic(music);

            return Ok(newMusic);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MusicVM>> UpdateMusic(int id, [FromBody] SaveMusicVM updateMusicVM)
        {
            SaveMusicVMValidator musicValidator = new SaveMusicVMValidator();
            ValidationResult result = await musicValidator.ValidateAsync(updateMusicVM);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            Music musicToBeUpdated = await MusicService.GetMusicById(id);

            if (musicToBeUpdated == null)
                return NotFound();

            Music updatedMusic = Mapper.Map<SaveMusicVM, Music>(updateMusicVM);
            await MusicService.UpdateMusic(musicToBeUpdated, updatedMusic);

            Music musicNewUpdate = await MusicService.GetMusicById(id);
            MusicVM music = Mapper.Map<Music, MusicVM>(musicNewUpdate);

            return Ok(music);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MusicVM>> DeleteMusic(int id)
        {
            Music music = await MusicService.GetMusicById(id);

            if (music == null)
                NotFound();

            await MusicService.DeleteMusic(music);

            return NoContent();
        }
    }
}
