using DAL.DbContexts;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        protected override DbSet<Company> DbSet
        {
            get { return _context.Companies; }
        }
        private readonly DefaultContext _context;
        public CompanyRepository(DefaultContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<Company>> GetByFiltersAsync(CompanyFilters filters)
        {
            IQueryable<Company> query = _context.Companies.Where(f =>
                   (filters.name == null || f.Name.Contains(filters.name))
            ).OrderBy(x => x.Name).AsQueryable();

            return await PagedList<Company>.CreateAsync(query, filters.PageNumber, filters.PageSize);
        }
    }
}
