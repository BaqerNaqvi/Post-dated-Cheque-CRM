using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace webapicore6.Models.Identity
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
