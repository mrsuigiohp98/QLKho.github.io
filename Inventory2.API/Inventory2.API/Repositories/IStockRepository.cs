using Inventory2.API.Helper;
using Inventory2.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Inventory2.API.Repositories.BaseResposiorty;

namespace Inventory2.API.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> ListAsync();

        Task<PagedList<Stock>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Stock> SaveAsync(Stock stock);


        Task<Stock> DeleteAsync(int id);


        Task<Stock> DeleteWithName(string name);

        Task<Stock> UpdateAsync(int id, Stock resource);
    }
}
