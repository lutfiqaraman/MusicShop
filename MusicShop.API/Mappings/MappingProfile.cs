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
            CreateMap<Composer, SaveComposerVM>();
            CreateMap<User, UserVM>();
            CreateMap<Composer, ComposerVM>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            //ViewModal to Domain
            CreateMap<MusicVM, Music>();
            CreateMap<ArtistVM, Artist>();
            CreateMap<SaveMusicVM, Music>();
            CreateMap<SaveComposerVM, Composer>();
            CreateMap<UserVM, User>();
            CreateMap<ComposerVM, Composer>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
