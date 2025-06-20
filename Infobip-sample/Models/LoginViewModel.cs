using System.ComponentModel.DataAnnotations;

namespace Infobip_sample.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email or Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
