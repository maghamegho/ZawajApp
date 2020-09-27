using System.ComponentModel.DataAnnotations;

namespace ZawajApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "max 8, min 4")]
        public string Password { get; set; }
    }
}