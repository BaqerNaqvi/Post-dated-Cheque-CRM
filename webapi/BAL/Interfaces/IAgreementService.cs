using DAL.Generic;
using DAL.Models;

namespace BAL.Interfaces
{
    public interface IAgreementService
    {
        Task<Agreement> GetAgreementAsync(int id);
        Task<List<Agreement>> GetAllAgreementsAsync();
        Task<List<Agreement>> GetAgreementsByCompanyIdAsync(int companyid);
        Task<List<Agreement>> GetByFilters(AgreementFilters AgreementFilters);
        Task<bool> AddAgreementAsync(Agreement Agreement);
        Task<bool> UpdateAgreementAsync(Agreement Agreement);
        Task<bool> DeleteAgreementAsync(int Id);
    }
}
