using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Core.Services;

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
    }
}
