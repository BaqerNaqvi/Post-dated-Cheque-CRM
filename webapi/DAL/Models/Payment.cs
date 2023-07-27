using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int AgreementId { get; set; }
        public int PaymentMethod { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? ChequeDueDate { get; set; }
        public int? SenderBankId { get; set; }
        public int? ReceiverBankId { get; set; }
        public int PaymentStatus { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? PaymentClearanceDate { get; set; }

        public virtual Agreement Agreement { get; set; } = null!;
        public virtual Bank? ReceiverBank { get; set; }
        public virtual Bank? SenderBank { get; set; }
    }
}
