using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Model.DTO
{
    public class UpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code Should be at least 3 length")]
        [MaxLength(3, ErrorMessage = "Code Should be at Max 3 length")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name Should be at Max 100")]
        public string Name { get; set; }

        public string? RegionImageurl { get; set; }

    }
}
