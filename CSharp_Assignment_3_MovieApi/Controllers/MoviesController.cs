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

namespace CSharp_Assignment_3_MovieApi.Controllers
{
    [Route("api/v1[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMovieService _movieService;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public MoviesController(MovieDbContext context, IMovieService service, ICharacterService characterService, IMapper mapper)
        {
            _context = context;
            _movieService = service;
            _characterService = characterService;
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

        /// <summary>
        /// Update movies with a movie id based of a list of ints supplied in the movieEditMovieDto object
        /// </summary>
        /// <param name="id">the Id for the movie you want updated</param>
        /// <param name="movieEditMoviesDto">the dto object data with the movie id you want the updated movies to have</param>
        /// <returns></returns>

        [HttpPatch("{id}/characters")]
        public async Task<ActionResult> PatchMovieCharacters(int id, MovieEditCharacterDto movieEditCharacterDto)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            try
            {
                // Get all the characters with ids in the list
                var MovieCharactersToUpdate = await _characterService.GetCharactersByIds(movieEditCharacterDto.CharacterIds);


                foreach (var character in MovieCharactersToUpdate)
                {
                    // Check if the record already exists in the join table
                    var existingRecord = _context.ChangeTracker.Entries<Dictionary<string, object>>()
                        .FirstOrDefault(x => x.Entity.TryGetValue("Id", out var movieObj)
                            && x.Entity.TryGetValue("Id", out var characterObj)
                            && movieObj == movie && characterObj == character);

                    if (existingRecord != null)
                    {
                        // The record already exists, so we don't need to do anything
                        continue;
                    }

                    // Add the new record to the join table
                    _context.Add(new Dictionary<string, object>
                    {
                        { "MovieId", movie.Id },
                        { "CharacterId", character.Id }
                    });
                }

                // Save the changes
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
