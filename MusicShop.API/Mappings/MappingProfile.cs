using AutoMapper;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to ViewModal
            CreateMap<Music, MusicVM>();
            CreateMap<Artist, ArtistVM>();

            //ViewModal to Domain
            CreateMap<MusicVM, Music>();
            CreateMap<ArtistVM, Artist>();
        }
    }
}
