using System.ComponentModel.DataAnnotations;

namespace LandSpace.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(50, ErrorMessage = "Max Length 50")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Compare password")]
        public string ConfirmPassword { get; set; }
    }
}
