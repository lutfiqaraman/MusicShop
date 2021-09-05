using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposerController : ControllerBase
    {
        private readonly IComposerService ComposerService;
        private readonly IMapper Mapper;

        public ComposerController(IComposerService composerService, IMapper mapper)
        {
            ComposerService = composerService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComposerVM>>> GetAllComposers()
        {
            IEnumerable<Composer> composers = await ComposerService.GetAllComposers();
            IEnumerable<ComposerVM> lstComposer =
                Mapper.Map<IEnumerable<Composer>, IEnumerable<ComposerVM>>(composers);

            return Ok(lstComposer);
        }
    }
}
