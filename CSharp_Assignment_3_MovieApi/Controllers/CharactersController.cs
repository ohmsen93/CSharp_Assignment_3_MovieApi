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
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _CharacterService;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService service, IMapper mapper)
        {
            _CharacterService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// GET: all Characters
        /// </summary>
        /// <returns>a List of Characters</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetAllCharacters()
        {
            var Characters = await _CharacterService.GetAllCharacters();
            var CharacterDto = _mapper.Map<IEnumerable<CharacterDto>>(Characters);
            return Ok(CharacterDto);
        }
        /// <summary>
        /// GET: Character by Id
        /// </summary>
        /// <param name="id">id for the Character you want</param>
        /// <returns>The requested Character</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacterById(int id)
        {
            var Character = await _CharacterService.GetCharacterById(id);
            if (Character == null)
            {
                return NotFound();
            }
            var CharacterDto = _mapper.Map<CharacterDto>(Character);
            return Ok(CharacterDto);
        }

        /// <summary>
        /// creates a new Character resource
        /// </summary>
        /// <param name="createCharacterDto">DTO with the data for the new Character to be created</param>
        /// <returns>201 Created response with the new Character data</returns>
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> PostCharacter(CreateCharacterDto createCharacterDto)
        {
            var Character = _mapper.Map<Character>(createCharacterDto);
            await _CharacterService.PostCharacter(Character);
            return CreatedAtAction(nameof(GetCharacterById), new { id = Character.Id }, Character);
        }
        /// <summary>
        /// deletes a Character resource by ID.
        /// </summary>
        /// <param name="id">The ID of the Character to be deleted</param>
        /// <returns>The deleted Character object</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CharacterDto>> DeleteCharacter(int id)
        {
            try
            {
                var Character = await _CharacterService.GetCharacterById(id);
                await _CharacterService.DeleteCharacter(id);
                return Ok(_mapper.Map<CharacterDto>(Character));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Update Character with given id to have the new data from the CharacterEditDto object
        /// </summary>
        /// <param name="id">the Id for the Character you want updated</param>
        /// <param name="CharacterEditDto">the dto object data you want the updated Character to have</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<CharacterDto>> PatchCharacter(int id, CharacterEditDto CharacterEditDto)
        {
            var Character = await _CharacterService.GetCharacterById(id);

            if (Character == null)
            {
                return NotFound();
            }
            _mapper.Map(CharacterEditDto, Character);
            try
            {
                Character = await _CharacterService.PatchCharacter(Character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var updatedCharacterDto = _mapper.Map<CharacterDto>(Character);

            return Ok(updatedCharacterDto);
        }
    }
}
