using BAL.Interfaces;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

namespace BAL.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly IBankRepository _repoBank;

        public PaymentService(IPaymentRepository repo, IBankRepository repoBank)
        {
            _repo = repo;
            _repoBank = repoBank;   
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

        public async Task<List<Payment>> ProcessImportedData(List<Dictionary<string, object>> paymentRows)
        {
            List < Payment > updatedPayments = new List < Payment >();
            foreach (var rowData in paymentRows)
            {                
                if (rowData.TryGetValue("DESCRIPTION", out var cellDescription) && cellDescription is string cellDescriptionValue)
                {
                    var parts = cellDescriptionValue.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3)
                    {
                        var chequeStatus = parts[0] + " " + parts[1];
                        decimal parsedAmount = 0;
                        string targetKey = (chequeStatus == "Returned Cheque") ? "DEBIT" : "CREDIT";

                        if (rowData.TryGetValue(targetKey, out var cellAmount) && cellAmount is string cellAmountValue)
                        {
                            decimal.TryParse(cellAmountValue, out parsedAmount);
                        }

                        var chequeNumber = parts[2];

                        var payment = _repo.GetPaymentByChequeNoAndAmountAsync(chequeNumber, parsedAmount).Result;

                        

                        if(payment != null)
                        {
                            payment.PaymentStatus = chequeStatus == "Returned Cheque" ? 3 : 1; //Bounced:Paid
                            if(payment.PaymentStatus == 1)
                            {
                                var bankName = string.Join(" ", parts.Skip(3));
                                var bank = _repoBank.GetBankByNameAsync(bankName).Result;
                                if (bank != null)
                                {
                                    payment.SenderBankId = bank.Id;
                                }
                            }

                            if (rowData.TryGetValue("TRAN DATE", out var cellDate) && cellDate is string cellDateValue)
                            {
                                if (DateTime.TryParseExact(cellDateValue, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                                {
                                    payment.PaymentClearanceDate = parsedDate;
                                }
                            }

                            if (rowData.TryGetValue("REF NO / CHQ NO", out var cellRef) && cellRef is string cellRefValue)
                            {
                                payment.StatementRef = cellRefValue;
                            }

                            payment.PaymentMethod = 0; //Cheque
                            payment.Description += Environment.NewLine +"=> "+ cellDescriptionValue;

                            updatedPayments.Add(payment);
                            _repo.Update(payment);
                        }
                    }
                }
            }

            if(updatedPayments.Count > 0)
                await _repo.SaveChangesAsync();
            return updatedPayments;
        }
    }
}