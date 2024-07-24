using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Model.Domain;
using NZwalks.API.Model.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepo walkRepo;

        public WalksController(IMapper mapper, IWalkRepo walkRepo)
        {
            this.mapper = mapper;
            this.walkRepo = walkRepo;
        }

        //Create MEthod :Post

        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalksDto addWalksDto)
        {

            
            
                var WalkDomainModel = mapper.Map<Walk>(addWalksDto);

                await walkRepo.createAsync(WalkDomainModel);

                var walkDto = mapper.Map<WalkDto>(WalkDomainModel);

                return Ok(walkDto);
            

        }


        [HttpGet]

        public async Task<IActionResult> GeTAll([FromQuery] string? filterOn, [FromQuery] string? filterQuerry, [FromQuery] string? SortBy, [FromQuery] bool IsAscending, [FromQuery] int PageNumber=1, [FromQuery] int PageSize=1000)
        {
            var walkDomainModel = await walkRepo.GetAllAsync(filterOn,filterQuerry,SortBy,IsAscending,PageNumber,PageSize);

            var walkdto = mapper.Map<List<WalkDto>>(walkDomainModel);

            return Ok(walkdto);
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

          var walkDomainModel= await walkRepo.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }


        [HttpPut]

        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
                var WalkDomainModel = mapper.Map<Walk>(updateWalkDto);
                WalkDomainModel = await walkRepo.UpdateAsync(id, WalkDomainModel);

                if (WalkDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(WalkDomainModel));
            
        } 

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var WalkDomainModel=await walkRepo.DeleteAsync(id);

            if(WalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(WalkDomainModel));
        }


    }
}
