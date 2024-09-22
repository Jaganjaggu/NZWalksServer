using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // .../api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksControler : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksControler(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE walk
        //POST: /api/walks

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            
                var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain model to Dto
                var domainDto = mapper.Map<WalkDto>(walkDomainModel);

                return Ok(domainDto);
         
            //Map or Convert DTO To Domain model
           
        }

        //GET Walks
        //GET: /appi/walks?filteron=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10

        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery,sortBy, 
                isAscending ?? true, pageNumber, pageSize);

            //Create an exception
            //throw new Exception("This is a new exception");

            //map domain to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //GET A walk by ID
        //GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.GetByIdAsync(id);

            if(walksDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain to Dto

            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }

        //Update Walk by id
        //PUT: /api/walks/{id}

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {

            //Map Dto to Domain

            var walkDomainModel = mapper.Map<Walk>(updateWalksRequestDto);

                walkDomainModel = await walkRepository.UpdateByIdAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //map domain to Dto

                return Ok(mapper.Map<WalkDto>(walkDomainModel));
          
            
        }


        //Delete a walk by ID
        //DELETE: /api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walksDomainModel = await walkRepository.DeleteByIdAsync(id);

            if(walksDomainModel == null)
            {
                return NotFound();
            }
            //map domain to dto
            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }
    }
}
