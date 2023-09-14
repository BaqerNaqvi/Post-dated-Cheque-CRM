using DAL.DbContexts;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class AgreementRepository : BaseRepository<Agreement>, IAgreementRepository
    {
        protected override DbSet<Agreement> DbSet
        {
            get { return _context.Agreements; }
        }
        private readonly DefaultContext _context;
        public AgreementRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Agreement>> GetByFiltersAsync(AgreementFilters filters)
        {
            IQueryable<Agreement> query = _context.Agreements.Where(f =>
                   (filters.companyId == null || filters.companyId == f.CompanyId)
            ).OrderBy(x => x.StartDate).AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<List<Agreement>> GetByCompanyIdAsync(int id)
        {
            IQueryable<Agreement> query = _context.Agreements.Where(f =>
               (id == f.CompanyId)
            ).OrderBy(x => x.StartDate).AsQueryable();

            return await query.ToListAsync();
        }
        
    }
}
