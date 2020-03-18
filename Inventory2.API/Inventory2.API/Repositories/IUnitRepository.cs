using Inventory2.API.Helper;
using Inventory2.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Inventory2.API.Repositories.BaseResposiorty;

namespace Inventory2.API.Repositories
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> ListAsync();

        Task<PagedList<Unit>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Unit> SaveAsync(Unit unit);

        Task<Unit> DeleteAsync(int id);

        Task<Unit> DeleteWithName(string name);

        Task<Unit> UpdateAsync(int id, Unit resource);
    }
}
