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
    public class InventoryRepository : BaseRepository, IInventoryRepository
    {
        public InventoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Inventory>> ListAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<PagedList<InventoryViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<InventoryViewModel> _query = from u in _context.Inventories
                                           join u2 in _context.Units on u.UnitId equals u2.Id
                                           join u3 in _context.Stocks on u.StockId equals u3.Id
                                           orderby u.Name
                                      select new InventoryViewModel 
                                      { 
                                          Id = u.Id,
                                          Name = u.Name,
                                          Soluong = u.Soluong,
                                          NoiSX = u.NoiSX,
                                          UnitId= u2.Id,
                                          UnitName= u2.Name,
                                          StockId = u3.Id,
                                          StockName = u3.Name
                                      };
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


            return await PagedList<InventoryViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Inventory> SaveAsync(Inventory _obj)
        {
            await _context.Inventories.AddAsync(_obj);
            await _context.SaveChangesAsync();

            return _obj;

        }


        public async Task<Inventory> DeleteAsync(int id)
        {
            var _obj = await _context.Inventories.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Inventories.Remove(_obj);

                await _context.SaveChangesAsync(); 
            }
            return _obj;
        }



        public async Task<Inventory> DeleteWithName(string name)
        {
            var _obj = await _context.Inventories.Where(o => o.Name == name).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _context.Inventories.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Inventory> UpdateAsync(int id, Inventory resource)
        {
            var _obj = await _context.Inventories.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Soluong = resource.Soluong;
                _obj.NoiSX = resource.NoiSX;
                _obj.UnitId = resource.UnitId;
                _obj.StockId = resource.StockId;

                await _context.SaveChangesAsync();
            }
            return _obj;
        }






    }

}
