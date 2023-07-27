using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Bank
    {
        public Bank()
        {
            PaymentReceiverBanks = new HashSet<Payment>();
            PaymentSenderBanks = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Branch { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? PoBox { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Payment> PaymentReceiverBanks { get; set; }
        public virtual ICollection<Payment> PaymentSenderBanks { get; set; }
    }
}
