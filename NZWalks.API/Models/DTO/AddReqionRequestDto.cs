using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddReqionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code should be min of 3 chars")]
        [MaxLength(3, ErrorMessage ="Code should be max of 3 chars")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should be max of 100 chars")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
