using DAL.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IAgreementRepository : IBaseRepository<Agreement, int>
    {
        Task<List<Agreement>> GetByFiltersAsync(AgreementFilters filters);
        Task<List<Agreement>> GetByCompanyIdAsync(int id);
    }
}
