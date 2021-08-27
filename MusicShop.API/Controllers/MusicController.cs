using Microsoft.AspNetCore.Mvc;
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
        public MusicController(IMusicService musicService)
        {
            MusicService = musicService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetAllMusics()
        {
            IEnumerable<Music> musics = await MusicService.GetAllWithArtists();
            return Ok(musics);
        }
    }
}
