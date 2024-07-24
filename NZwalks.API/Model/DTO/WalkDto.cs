namespace NZwalks.API.Model.DTO
{
    public class WalkDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Lenghtinkm { get; set; }

        public string? WalkImageUrl { get; set; }


        public RegionDTO Region { get; set; }

        public DifficDtoulty Difficulty { get; set; }
    }
}
