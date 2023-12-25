using AutoMapper;
using Cooking.API.Models;
using Cooking.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Text.Json;

namespace Cooking.API.Controllers
{
    [ApiController]
    [Route("api/cookwares")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CookwareController : ControllerBase
    {
        private readonly ICookingRepository _cookingRepository;
        private readonly IMapper _mapper;
        const int maxCookwarePageSize = 20;

        public CookwareController(ICookingRepository cookingRepository, IMapper mapper)
        {
            _cookingRepository = cookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<CookwareDto>), Description = "Ok", HttpStatusCode = "200")]
        public async Task<ActionResult<IEnumerable<CookwareDto>>> GetCookwares(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize > maxCookwarePageSize)
            {
                pageSize = maxCookwarePageSize;
            }
            var (cookwareEntities, paginationMetaData) = await _cookingRepository.GetCookwaresAsync(name, searchQuery, pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<CookwareDto>>(cookwareEntities));
        }

        [HttpGet("{id}", Name = "GetCookware")]
        [ResponseType(typeof(CookwareDto), Description = "Ok", HttpStatusCode = "200")]
        public async Task<ActionResult<CookwareDto>> GetCookware(int id)
        {
            var cookware = await _cookingRepository.GetCookwareAsync(id);
            if(cookware == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CookwareDto>(cookware));
        }

        [HttpPost]
        [ResponseType(typeof (CookwareDto), Description ="Created", HttpStatusCode = "201")]
        public async Task<ActionResult<CookwareDto>> CreateCookWare(CookwareForCreationDto cookware)
        {
            var finalCookware = _mapper.Map<Entities.Cookware>(cookware);
            _cookingRepository.AddCookware(finalCookware);
            await _cookingRepository.SaveChangesAsync();
            var createdCookwareToReturn = _mapper.Map<Models.CookwareDto>(finalCookware);
            return CreatedAtRoute("GetCookware",
                new
                {
                    id = createdCookwareToReturn.Id
                },
                createdCookwareToReturn);
        }

        [HttpPut("{id}")]
        [ResponseType(HttpStatusCode.NoContent, typeof(void), Description = "NoContent")]
        public async Task<ActionResult> UpdateCookware(int id, CookwareForUpdateDto cookware)
        {
            var cookwareEntity = await _cookingRepository.GetCookwareAsync(id);
            if(cookwareEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(cookware, cookwareEntity);
            await _cookingRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ResponseType(HttpStatusCode.NoContent, typeof(void), Description = "NoContent")]
        public async Task<ActionResult> PartiallyUpdateCookware(int id, JsonPatchDocument<CookwareForUpdateDto> patchDocument)
        {
            var cookware = await _cookingRepository.GetCookwareAsync(id);
            if (cookware == null)
            {
                return NotFound();
            }
            var cookwareToPatch = _mapper.Map<CookwareForUpdateDto>(cookware);
            patchDocument.ApplyTo(cookwareToPatch, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!TryValidateModel(cookwareToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(cookwareToPatch, cookware);
            await _cookingRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ResponseType(HttpStatusCode.NoContent, typeof(void), Description = "NoContent")]
        public async Task<ActionResult> DeleteCookware(int id)
        {
            var cookware = await _cookingRepository.GetCookwareAsync(id);
            if (cookware == null)
            {
                return NotFound();
            }
            _cookingRepository.DeleteCookware(cookware);
            await _cookingRepository.SaveChangesAsync();
            return NoContent();
        }

    }
}
