using DAL.Generic;
using DAL.Models;

namespace BAL.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetByIdAsync(int id);
        Task<List<Company>> GetAllAsync();
        Task<List<Company>> GetByFilters(CompanyFilters filters);
        Task<bool> AddAsync(Company company);
        Task<bool> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int Id);
    }
}
