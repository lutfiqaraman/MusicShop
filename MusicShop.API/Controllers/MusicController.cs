﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
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

    }
}
