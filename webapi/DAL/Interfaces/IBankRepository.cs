﻿using DAL.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IBankRepository : IBaseRepository<Bank, int>
    {
        Task<Bank> GetBankByNameAsync(string bankName);
    }
}
