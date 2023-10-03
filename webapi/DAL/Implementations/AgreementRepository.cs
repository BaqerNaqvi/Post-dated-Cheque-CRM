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
                    && (
                        (filters.month == null || filters.year == null) || // No month or year filter
                        (
                            (filters.year >= f.StartDate.Year && filters.year <= f.EndDate.Year) && // Year is within range
                                (
                                    (filters.year == f.StartDate.Year && filters.year == f.EndDate.Year && filters.month >= f.StartDate.Month && filters.month <= f.EndDate.Month) || // Same year
                                    (filters.year == f.StartDate.Year && filters.year != f.EndDate.Year && filters.month >= f.StartDate.Month) || // Different end year
                                    (filters.year != f.StartDate.Year && filters.year == f.EndDate.Year && filters.month <= f.EndDate.Month) || // Different start year
                                    (filters.year != f.StartDate.Year && filters.year != f.EndDate.Year) // Different start and end year
                                )
                        )
                    )
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
