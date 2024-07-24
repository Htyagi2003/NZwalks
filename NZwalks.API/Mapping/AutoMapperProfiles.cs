using AutoMapper;
using NZwalks.API.Model.Domain;
using NZwalks.API.Model.DTO;

namespace NZwalks.API.Mapping
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<AddRegionDto,Region>().ReverseMap();  
            CreateMap<UpdateDto,Region>().ReverseMap();  
            CreateMap<AddWalksDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficDtoulty>().ReverseMap();
            CreateMap<UpdateWalkDto,Walk>().ReverseMap();
        }
    }
}
