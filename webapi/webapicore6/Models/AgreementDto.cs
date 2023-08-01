using DAL.Models;

namespace webapicore6.Models
{
    public class AgreementDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? Floor { get; set; }
        public string? OfficeNumber { get; set; }
        public string? Section { get; set; }
        public string? WorkStation { get; set; }
        public string? Branch { get; set; }
    }
}
