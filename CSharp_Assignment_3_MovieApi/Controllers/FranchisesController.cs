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
using CSharp_Assignment_3_MovieApi.Models.Dto;
using AutoMapper;

namespace CSharp_Assignment_3_MovieApi.Controllers
{
    [Route("api/v1[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IFranchiseService _franchiseService;
        private readonly IMapper _mapper;

        //public FranchisesController(MovieDbContext context)
        //{
        //    _context = context;
        //}
        public FranchisesController(MovieDbContext context, IFranchiseService service, IMapper mapper)
        {
            _context = context;
            _franchiseService = service;
            _mapper = mapper;
        }

        /// <summary>
        /// GET: all Franchises
        /// </summary>
        /// <returns>a List of Franchises</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetAllFranchises()
        {
            var franchises = await _franchiseService.GetAllFranchises();
            var franchiseDto = _mapper.Map<IEnumerable<FranchiseDto>>(franchises);
            return Ok(franchiseDto);
        }
        /// <summary>
        /// GET: Franchise by Id
        /// </summary>
        /// <param name="id">id for the Franchise you want</param>
        /// <returns>The requested franchise</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchiseById(int id)
        {
            var franchise = await _franchiseService.GetFranchiseById(id);
            if (franchise == null)
            {
                return NotFound();
            }
            var franchiseDto = _mapper.Map<FranchiseDto>(franchise);
            return Ok(franchiseDto);
        }

        /// <summary>
        /// creates a new Franchise resource
        /// </summary>
        /// <param name="createFranchiseDto">DTO with the data for the new Franchise to be created</param>
        /// <returns>201 Created response with the new Franchise data</returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseDto>> PostFranchise(CreateFranchiseDto createFranchiseDto)
        {
            var franchise = _mapper.Map<Franchise>(createFranchiseDto);
            await _franchiseService.PostFranchise(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = franchise.Id }, franchise);
        }
        /// <summary>
        /// deletes a Franchise resource by ID.
        /// </summary>
        /// <param name="id">The ID of the Franchise to be deleted</param>
        /// <returns>The deleted Franchise object</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<FranchiseDto>> DeleteFranchise(int id)
        {
            try
            {
                var franchise = await _franchiseService.GetFranchiseById(id);
                await _franchiseService.DeleteFranchise(id);
                return Ok(_mapper.Map<FranchiseDto>(franchise));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Update franchise with given id to have the new data from the franchiseEditDto object
        /// </summary>
        /// <param name="id">the Id for the franchise you want updated</param>
        /// <param name="franchiseEditDto">the dto object data you want the updated franchise to have</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult<FranchiseDto>> PatchFranchise(int id, FranchiseEditDto franchiseEditDto)
        {
            var franchise = await _franchiseService.GetFranchiseById(id);

            if (franchise == null)
            {
                return NotFound();
            }
            _mapper.Map(franchiseEditDto, franchise);
            try
            {
                franchise = await _franchiseService.PatchFranchise(franchise);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var updatedFranchiseDto = _mapper.Map<FranchiseDto>(franchise);

            return Ok(updatedFranchiseDto);
        }
    }
}
