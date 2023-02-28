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
            // Add the missing mapping configuration between FranchiseDto and Franchise
            CreateMap<FranchiseDto, Franchise>()
                .ForMember(franchise => franchise.Id, options => options.Ignore());
            CreateMap<FranchiseEditDto, Franchise>()
                .ForMember(dest => dest.Movies, options => options.Ignore())
                .ForMember(dest => dest.Id, options => options.Ignore());
        }
    }
}
