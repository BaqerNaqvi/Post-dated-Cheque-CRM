using DAL.Generic;
using DAL.Models;

namespace BAL.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentAsync(int id);
        Task<List<Payment>> GetAllPaymentsAsync();
        Task<List<Payment>> GetPaymentByAgreementIdAsync(int agreementid);
        Task<List<Payment>> GetByFilters(PaymentFilters paymentFilters);
        Task<bool> AddPaymentAsync(Payment Payment);
        Task<bool> UpdatePaymentAsync(Payment Payment);
        Task<bool> DeletePaymentAsync(int Id);
        Task<List<Payment>> ProcessImportedData(List<Dictionary<string, object>> paymentRows);
    }
}
