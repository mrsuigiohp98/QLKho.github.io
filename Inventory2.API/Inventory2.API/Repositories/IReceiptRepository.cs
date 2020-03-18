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
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> ListAsync();

        Task<PagedList<ReceiptViewModel>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Receipt> SaveAsync(Receipt receipt);

        Task<Receipt> DeleteAsync(int id);

        Task<Receipt> DeleteWithName(string name);

        Task<Receipt> UpdateAsync(int id, Receipt resource);
    }
}
