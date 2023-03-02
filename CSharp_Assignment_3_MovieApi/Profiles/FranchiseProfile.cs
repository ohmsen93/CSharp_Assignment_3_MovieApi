﻿using AutoMapper;
using CSharp_Assignment_3_MovieApi.Models;

namespace CSharp_Assignment_3_MovieApi.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            
            CreateMap<CreateFranchiseDto, Franchise>();

            // Mappings between Franchise and FranchiseDto
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(movieDomain => movieDomain.Movies.Select(movie => $"{movie.Id}").ToList()));
            
            // Add the missing mapping configuration between FranchiseDto and Franchise
            CreateMap<FranchiseDto, Franchise>()
                .ForMember(franchise => franchise.Id, options => options.Ignore());

            // Mappings between FranchiseEditDto and Franchise
            CreateMap<FranchiseEditDto, Franchise>()
                .ForMember(dest => dest.Movies, options => options.Ignore())
                .ForMember(dest => dest.Id, options => options.Ignore());

            // Mappings between FranchiseEditMovieDto and Tuple<int, IEnumerable<int>>
            CreateMap<FranchiseEditMovieDto, Tuple<int, IEnumerable<int>>>()
                .ConstructUsing(dto => new Tuple<int, IEnumerable<int>>(0, dto.MovieIds));
        }
    }
}
