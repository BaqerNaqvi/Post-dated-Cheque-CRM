using BAL.Interfaces;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _repo.GetAll().ToListAsync();
        }

        public async Task<bool> AddAsync(Company Company)
        {
            try
            {
                _repo.Create(Company);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<bool> UpdateAsync(Company Company)
        {
            try
            {
                _repo.Update(Company);

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
                var Company = await _repo.GetByIdAsync(Id);
                _repo.Delete(Company);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}