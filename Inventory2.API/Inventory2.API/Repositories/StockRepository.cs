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
    public class StockRepository : BaseRepository, IStockRepository
    {
        public StockRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Stock>> ListAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<PagedList<Stock>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Stock> _query = from u in _context.Stocks
                                      orderby u.Name
                                      select new Stock { Id = u.Id, Name = u.Name };
            // Search
            if (pagingParams.SearchValue == "name")
            {
                if (string.IsNullOrEmpty(pagingParams.SearchKey) == false)
                {
                    _query = _query.Where(o => o.Name.Contains(pagingParams.SearchKey));
                }
            }

            if (pagingParams.SearchValue == "id")
            {
                if (string.IsNullOrEmpty(pagingParams.SearchKey) == false)
                {
                    int _id = Convert.ToInt32(pagingParams.SearchKey);

                    _query = _query.Where(o => o.Id == _id);
                }
            }

            //Sort 
            if (pagingParams.SortKey == "name")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Name);
                else
                    _query = _query.OrderByDescending(o => o.Name);
            }

            if (pagingParams.SortKey == "id")
            {
                if (pagingParams.SortValue == "ascend")

                    _query = _query.OrderBy(o => o.Id);
                else
                    _query = _query.OrderByDescending(o => o.Id);
            }

            return await PagedList<Stock>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Stock> SaveAsync(Stock _obj)
        {
            await _context.Stocks.AddAsync(_obj);
            await _context.SaveChangesAsync();

            return _obj;

        }

        public async Task<Stock> DeleteAsync(int id)
        {
            var _obj = await _context.Stocks.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Stocks.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }


        public async Task<Stock> DeleteWithName(string name)
        {
            var _obj = await _context.Stocks.Where(o => o.Name == name).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _context.Stocks.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }


        public async Task<Stock> UpdateAsync(int id, Stock resource)
        {
            var _obj = await _context.Stocks.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;


                await _context.SaveChangesAsync();
            }
            return _obj;
        }


    }
}
