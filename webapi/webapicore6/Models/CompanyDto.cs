using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace webapicore6.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
