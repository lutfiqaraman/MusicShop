using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections;
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
    }
}
