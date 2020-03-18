using Inventory2.API.Helper;
using Inventory2.API.Models;
using Inventory2.API.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Inventory2.API.Repositories.BaseResposiorty;

namespace Inventory2.API.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> ListAsync();

        Task<PagedList<InventoryViewModel>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Inventory> SaveAsync(Inventory inventory);

        Task<Inventory> DeleteAsync(int id);

        Task<Inventory> DeleteWithName(string name);

        Task<Inventory> UpdateAsync(int id, Inventory  resource);

    }
}
