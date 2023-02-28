using AutoMapper;
using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<CreateCharacterDto, Character>();
            CreateMap<Character, CharacterDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(movieDomain => movieDomain.Movies.Select(movie => $"api/v1/movies/{movie.Id}").ToList()));
            // Add the missing mapping configuration between CharacterDto and Character
            CreateMap<CharacterDto, Character>()
                .ForMember(Character => Character.Id, options => options.Ignore());
            CreateMap<CharacterEditDto, Character>()
                .ForMember(dest => dest.Movies, options => options.Ignore())
                .ForMember(dest => dest.Id, options => options.Ignore());
        }
    }
}
