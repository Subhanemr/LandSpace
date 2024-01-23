using System.ComponentModel.DataAnnotations;

namespace LandSpace.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
