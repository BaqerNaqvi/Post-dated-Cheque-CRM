using DAL.DbContexts;
using DAL.Generic;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        protected override DbSet<Bank> DbSet
        {
            get { return _context.Banks; }
        }
        private readonly DefaultContext _context;
        public BankRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Bank> GetBankByNameAsync(string bankName)
        {
            Bank bank = await _context.Banks.FirstOrDefaultAsync(f =>
                bankName == f.Name);

            return bank;
        }
    }
}
