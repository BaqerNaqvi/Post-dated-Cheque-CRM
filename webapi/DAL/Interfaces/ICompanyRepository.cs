using DAL.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company, int>
    {
        Task<List<Company>> GetByFiltersAsync(CompanyFilters filters);
    }
}
