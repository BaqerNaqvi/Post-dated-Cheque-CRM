namespace webapicore6.Models
{
    public class PaymentStatsWrapper
    {
        public List<PaymentListDto> Payments { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalDue { get; set; }
    }
}
