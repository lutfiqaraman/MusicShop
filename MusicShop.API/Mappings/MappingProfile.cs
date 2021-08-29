using AutoMapper;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;

namespace MusicShop.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to ViewModal
            CreateMap<Music, MusicVM>();
            CreateMap<Artist, ArtistVM>();
            CreateMap<Music, SaveMusicVM>();

            //ViewModal to Domain
            CreateMap<MusicVM, Music>();
            CreateMap<ArtistVM, Artist>();
            CreateMap<SaveMusicVM, Music>();
        }
    }
}
