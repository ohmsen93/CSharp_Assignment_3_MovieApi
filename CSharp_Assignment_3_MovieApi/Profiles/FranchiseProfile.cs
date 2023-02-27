using AutoMapper;
using CSharp_Assignment_3_MovieApi.Models;
using CSharp_Assignment_3_MovieApi.Models.Dto;

namespace CSharp_Assignment_3_MovieApi.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile() 
        {
            CreateMap<CreateFranchiseDto, Franchise>();
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(movieDomain => movieDomain.Movies.Select(movie => $"api/v1/movies/{movie.Id}").ToList()));
        }
    }
}
