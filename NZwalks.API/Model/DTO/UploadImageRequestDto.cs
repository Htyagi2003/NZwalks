﻿using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Model.DTO
{
    public class UploadImageRequestDto
    {
        [Required]
        public IFormFile File { get; set; }


        [Required]

        public string FileName { get; set; }    

        public string? FileDescription { get; set; }
    }
}
