using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMakeRepository
    {
        Task<List<Make>?> GetMakes(bool includeRelated = true);
    }
}