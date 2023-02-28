using AutoMapper;
using CSharp_Assignment_3_MovieApi.Models.Dto;
using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        {
            CreateMap<CreateMovieDto, Movie>();
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Characters, options =>
                options.MapFrom(characterDomain => characterDomain.Characters.Select(character => $"api/v1/characters/{character.Id}").ToList()));
            // Add the missing mapping configuration between MovieDto and Movie
            CreateMap<MovieDto, Movie>()
                .ForMember(character => character.Id, options => options.Ignore());
            CreateMap<MovieEditDto, Movie>()
                .ForMember(dest => dest.Characters, options => options.Ignore())
                .ForMember(dest => dest.Id, options => options.Ignore());
        }
    }
}
