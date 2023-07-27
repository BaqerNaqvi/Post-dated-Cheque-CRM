using BAL.Interfaces;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Implementation
{
    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository _repo;
        public AgreementService(IAgreementRepository repo)
        {
            _repo = repo;
        }

        public async Task<Agreement> GetAgreementAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Agreement>> GetAllAgreementsAsync()
        {
            return await _repo.GetAll().ToListAsync();
        }

        public async Task<List<Agreement>> GetAgreementsByCompanyIdAsync(int compId)
        {
            return await _repo.GetByCompanyIdAsync(compId);
        }

        public async Task<bool> AddAgreementAsync(Agreement Agreement)
        {
            try
            {
                _repo.Create(Agreement);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }            
        }

        public async Task<bool> UpdateAgreementAsync(Agreement Agreement)
        {
            try
            {
                _repo.Update(Agreement);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteAgreementAsync(int Id)
        {
            try
            {
                var Agreement = await _repo.GetByIdAsync(Id);
                _repo.Delete(Agreement);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Agreement>> GetByFilters(AgreementFilters AgreementFilters)
        {
            return await _repo.GetByFiltersAsync(AgreementFilters);
        }
    }
}