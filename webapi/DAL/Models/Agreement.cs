using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Agreement
    {
        public Agreement()
        {
            Payments = new HashSet<Payment>();
        }

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

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
