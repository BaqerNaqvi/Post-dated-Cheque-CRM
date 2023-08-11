using DAL.DbContexts;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        protected override DbSet<Payment> DbSet
        {
            get { return _context.Payments; }
        }
        private readonly DefaultContext _context;
        public PaymentRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetByFiltersAsync(PaymentFilters paymentFilters)
        {
            IQueryable<Payment> query = _context.Payments.Where(f =>
                   (paymentFilters.agreementId == null || paymentFilters.agreementId == f.AgreementId)
                && (paymentFilters.companyId == null || paymentFilters.companyId == f.Agreement.CompanyId)
                && (paymentFilters.bankId == null || paymentFilters.bankId == f.SenderBankId)
                && (paymentFilters.paymentMethodId == null || paymentFilters.paymentMethodId == f.PaymentMethod)
                && (paymentFilters.month == null || paymentFilters.month == f.PaymentDueDate.Month || f.ChequeDueDate.Value.Month == paymentFilters.month)
                && (paymentFilters.year == null || paymentFilters.year == f.PaymentDueDate.Year || f.ChequeDueDate.Value.Year == paymentFilters.year)
                && (paymentFilters.branch == null || paymentFilters.branch == "" || paymentFilters.branch == f.Agreement.Branch)
                ).OrderBy(x => x.ChequeDueDate).AsQueryable();

            return await PagedList<Payment>.CreateAsync(query, paymentFilters.PageNumber, paymentFilters.PageSize);
        }

        public async Task<List<Payment>> GetPaymentByAgreementIdAsync(int agreementid)
        {
            IQueryable<Payment> query = _context.Payments.Where(f =>
               (agreementid == f.AgreementId)
            ).OrderBy(x => x.ChequeDueDate).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<Payment> GetPaymentByChequeNoAndAmountAsync(string chequeNo, decimal amount)
        {
            Payment payment = await _context.Payments.FirstOrDefaultAsync(f =>
                chequeNo == f.ChequeNo && amount == f.Amount);

            return payment;
        }
    }
}
