using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        //----------------------------------------------------------------------------------------
        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Get All Region Action method was invoked");
            /*// simple code to get regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id= Guid.NewGuid(),
                    Name= "Auclin Region",
                    Code= "AKL",
                    RegionImageUrl="https://www.thewowstyle.com/wp-content/uploads/2015/01/nature-images..jpg"
                },
                new Region
                {
                    Id= Guid.NewGuid(),
                    Name= "Wellington Region",
                    Code= "WLG",
                    RegionImageUrl="https://www.thewowstyle.com/wp-content/uploads/2015/01/nature-images..jpg"
                }

            };*/

            //Method using without automapper___________________
            //Get data from database - Domain Models
            //var regionsDomain = await regionRepository.GetAllAsync();

            //Map domain models to DTO

            //var regionDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}

            //Return DTOs
            //return Ok(regionDto);
            //Method using without automapper___________________

            //Using Automapper_______________________________
            var regionDomain = await regionRepository.GetAllAsync();
            
            logger.LogInformation($"Finished GetAllRegions data:{JsonSerializer.Serialize(regionDomain)}");

            return Ok(mapper.Map<List<RegionDto>>(regionDomain));
            //Using Automapper_______________________________

        }

        //----------------------------------------------------------------------------------------

        //GET SINGLE REGION (GET Region By ID)
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
           // var regions = dbContext.Regions.Find(id);
            //or


            //Get Region Domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            //in this we can search code or any other not only id

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert domain models to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};


            //Using Automapper_______________________________

            return Ok(mapper.Map<RegionDto>(regionDomain));

            //Using Automapper_______________________________

        }

        //----------------------------------------------------------------------------------------

        //POST to create new region
        //POST: http://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] AddReqionRequestDto addReqionRequestDto)
        {
            //Map or convert DTO to Domain model
            //var regionDomainModel = new Region
            //{
            //    Code = addReqionRequestDto.Code,
            //    Name = addReqionRequestDto.Name,
            //    RegionImageUrl = addReqionRequestDto.RegionImageUrl
            //};

          
                var regionDomainModel = mapper.Map<Region>(addReqionRequestDto);


                //Use Domain Model to create Region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain Model to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
           
        }

        //----------------------------------------------------------------------------------------

        //Update Region
        //PUT : http://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to domain model
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl= updateRegionRequestDto.RegionImageUrl
            //};

          
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //Check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Convert Domain model to DTO
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(regionDto);
          
           


        }

        //----------------------------------------------------------------------------------------

        //Delete a Region
        //DELETE : http://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Return deleted region back
            //Convert Domain model to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}
