namespace webapicore6.Enums
{
    public enum PaymentStatus
    {
        Pending,   // The payment is due but has not been received yet
        Paid,      // The payment has been received
        Late,      // The payment is overdue
        Bounced,   // The cheque payment has been rejected by the bank
        Cancelled // The payment was cancelled for some reason
    }
}
