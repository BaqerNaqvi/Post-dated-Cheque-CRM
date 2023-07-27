using DAL.Generic;
using DAL.Models;

namespace BAL.Interfaces
{
    public interface IBankService
    {
        Task<Bank> GetByIdAsync(int id);
        Task<List<Bank>> GetAllAsync();
        Task<bool> AddAsync(Bank bank);
        Task<bool> UpdateAsync(Bank bank);
        Task<bool> DeleteAsync(int Id);
    }
}
