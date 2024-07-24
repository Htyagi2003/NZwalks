using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Model.Domain;
using NZwalks.API.Model.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers

{
    //https://localhost:portnumber/api/region
    [Route("api/[controller]")]
    [ApiController]
    


    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBcontext dbContext;
        private readonly IRegionRepo regionRepo;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDBcontext dbContext,IRegionRepo regionRepo,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepo = regionRepo;
            this.mapper = mapper;
        }
        //GET:https:://portnumber/api/regions
        [HttpGet]
        [Authorize(Roles ="Reader")]


        //we make method asynchronous so our main thread doesn't block out due to long execution of databases querrt 
        //or other request
        public async Task<IActionResult> GetAll()
        {   //getting data from database
            var regionsDomain = await regionRepo.GetAllAsync();

            //map domain model or database to dtos fro abtsraction return me hi krdia to look clean and concise

            // return dto back to client
            return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        //get single region by id
        ////https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]


        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            //getting data from domain model-databases
            var regionDomain = await regionRepo.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //mapping to dto
            return Ok(mapper.Map<RegionDTO>(regionDomain));

        }


        //to create Region
        //Post:https://localhost:portnumber/api/region/{id}

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto)
        {
            

                //Convert Dto to domain model

                var regionDomainModel = mapper.Map<Region>(addRegionDto);

                //Domain help to changes datbases

                regionDomainModel = await regionRepo.createAsync(regionDomainModel);

                //created dto to sent back to client
                var regionDto = mapper.Map<RegionDTO>(regionDomainModel);


                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            
           

        }


        //now update method
        //put:https://............./{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]


        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDto updateDto)
        {
            //Map this to domain Model

            

                var regiondomainnodel = mapper.Map<Region>(updateDto);

                //check if exist to change
                regiondomainnodel = await regionRepo.updateAsync(id, regiondomainnodel);

                if (regiondomainnodel == null)
                {
                    return NotFound();
                }


                //create dto

                var regiondto = mapper.Map<RegionDTO>(regiondomainnodel);

                return Ok(regiondto);
            

        }


        //To Delete a region
        //Delete:https://......{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]


        public async Task<IActionResult> delete([FromRoute] Guid id)
        {

            var regionDomainModel = await regionRepo.deleteAsync(id);

            if (regionDomainModel == null)
            {

                return NotFound();

            }


            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }
    }
}
