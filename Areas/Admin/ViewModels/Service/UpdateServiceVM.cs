using System.ComponentModel.DataAnnotations;

namespace LandSpace.Areas.Admin.ViewModels
{
    public class UpdateServiceVM
    {
        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(150, ErrorMessage = "Max Length 150")]
        public string Description { get; set; }

        public string? Img { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
