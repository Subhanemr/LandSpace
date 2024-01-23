using System.ComponentModel.DataAnnotations;

namespace LandSpace.Areas.Admin.ViewModels
{
    public class CreateSettingsVM
    {
        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(150, ErrorMessage = "Max Length 150")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Is Required")]
        [MinLength(3, ErrorMessage = "Min Length 3")]
        [MaxLength(150, ErrorMessage = "Max Length 150")]
        public string Value { get; set; }
    }
}
