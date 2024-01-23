using System.ComponentModel.DataAnnotations;

namespace LandSpace.Areas.Admin.ViewModels
{
    public class CreateServiceVM
    {
        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public IFormFile Photo { get; set; }
    }
}
