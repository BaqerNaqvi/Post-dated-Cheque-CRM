using BAL.Interfaces;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Implementation
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repo;
        public BankService(IBankRepository repo)
        {
            _repo = repo;
        }

        public async Task<Bank> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Bank>> GetAllAsync()
        {
            return await _repo.GetAll().ToListAsync();
        }

        public async Task<bool> AddAsync(Bank Bank)
        {
            try
            {
                _repo.Create(Bank);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<bool> UpdateAsync(Bank Bank)
        {
            try
            {
                _repo.Update(Bank);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var Bank = await _repo.GetByIdAsync(Id);
                _repo.Delete(Bank);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}