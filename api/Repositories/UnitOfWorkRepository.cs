using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;

namespace api.Repositories
{
    public class UnitOfWorkRepository(ApplicationDBContext context) : IUnitOfWorkRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}