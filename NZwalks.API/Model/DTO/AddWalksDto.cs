using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Model.DTO
{
    public class AddWalksDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name Should be at Max 100 length")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description Should be at Max 1000 length")]
        public string Description { get; set; }
        [Required]

        [Range(0,50)]

        public double Lenghtinkm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]

        public Guid DifficultyId { get; set; }

        [Required]

        public Guid RegionId { get; set; }

    }
}
