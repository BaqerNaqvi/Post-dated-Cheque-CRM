using DAL.Models;

namespace webapicore6.Models
{
    public class BankDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Branch { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? PoBox { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
    }
}
