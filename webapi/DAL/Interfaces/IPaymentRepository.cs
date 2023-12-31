﻿using DAL.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment, int>
    {
        Task<List<Payment>> GetByFiltersAsync(PaymentFilters paymentFilters);
        Task<List<Payment>> GetPaymentByAgreementIdAsync(int agreeId);
        Task<Payment> GetPaymentByChequeNoAndAmountAsync(string chequeNo, decimal amount, int month, int year);
    }
}
