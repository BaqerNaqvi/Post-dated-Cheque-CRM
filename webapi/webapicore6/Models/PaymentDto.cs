using DAL.Models;
namespace webapicore6.Models
{
    public class PaymentDto
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
    }
}
