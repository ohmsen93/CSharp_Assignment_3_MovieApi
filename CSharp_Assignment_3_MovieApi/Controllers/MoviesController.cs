using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharp_Assignment_3_MovieApi.DatabaseContext;
using CSharp_Assignment_3_MovieApi.Models;
using System.Reflection;
using System.Net.Mime;
using CSharp_Assignment_3_MovieApi.Services;
using AutoMapper;
using CSharp_Assignment_3_MovieApi.Models.Dto.Movie;

namespace CSharp_Assignment_3_MovieApi.Controllers
{
    [Route("api/v1[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService service, IMapper mapper)
        {
            _movieService = service;
            _mapper = mapper;
        }
        /// <summary>
        /// GET: all Movies
        /// </summary>
        /// <returns>a List of Movies</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            var movieDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(movieDto);
        }
        /// <summary>
        /// GET: Movie by Id
        /// </summary>
        /// <param name="id">id for the Movie you want</param>
        /// <returns>The requested movie</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }
        /// <summary>
        /// creates a new Movie resource
        /// </summary>
        /// <param name="createMovieDto">DTO with the data for the new Movie to be created</param>
        /// <returns>201 Created response with the new Movie data</returns>
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            await _movieService.PostMovie(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }
        /// <summary>
        /// deletes a Movie resource by ID.
        /// </summary>
        /// <param name="id">The ID of the Movie to be deleted</param>
        /// <returns>The deleted Movie object</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDto>> DeleteMovie(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieById(id);
                await _movieService.DeleteMovie(id);
                return Ok(_mapper.Map<MovieDto>(movie));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Update movie with given id to have the new data from the movieEditDto object
        /// </summary>
        /// <param name="id">the Id for the movie you want updated</param>
        /// <param name="movieEditDto">the dto object data you want the updated Movie to have</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<MovieDto>> PatchMovie(int id, MovieEditDto movieEditDto)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }
            _mapper.Map(movieEditDto, movie);
            try
            {
                movie = await _movieService.PatchMovie(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var updatedMovieDto = _mapper.Map<MovieDto>(movie);

            return Ok(updatedMovieDto);
        }
    }
}
