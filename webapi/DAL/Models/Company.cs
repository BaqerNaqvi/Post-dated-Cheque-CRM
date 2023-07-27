using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            Agreements = new HashSet<Agreement>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
