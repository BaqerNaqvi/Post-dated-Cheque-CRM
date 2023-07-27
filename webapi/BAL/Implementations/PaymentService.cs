using BAL.Interfaces;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _repo.GetAll().ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentByAgreementIdAsync(int agreementid)
        {
            return await _repo.GetPaymentByAgreementIdAsync(agreementid);
        }

        public async Task<bool> AddPaymentAsync(Payment Payment)
        {
            try
            {
                _repo.Create(Payment);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<bool> UpdatePaymentAsync(Payment Payment)
        {
            try
            {
                _repo.Update(Payment);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeletePaymentAsync(int Id)
        {
            try
            {
                var Payment = await _repo.GetByIdAsync(Id);
                _repo.Delete(Payment);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Payment>> GetByFilters(PaymentFilters paymentFilters)
        {
            return await _repo.GetByFiltersAsync(paymentFilters);
        }
    }
}