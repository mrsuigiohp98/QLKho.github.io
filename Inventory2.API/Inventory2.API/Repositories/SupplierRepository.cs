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
    public class SupplierRepository :BaseRepository, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Supplier>> ListAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<PagedList<Supplier>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<Supplier> _query = from u in _context.Suppliers
                                          orderby u.Name
                                          select new Supplier { Id = u.Id, Name = u.Name, Diachi = u.Diachi, Sdt = u.Sdt };
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

            return await PagedList<Supplier>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Supplier> SaveAsync(Supplier _obj)
        {
            await _context.Suppliers.AddAsync(_obj);
            await _context.SaveChangesAsync();

            return _obj;

        }


        public async Task<Supplier> DeleteAsync(int id)
        {
            var _obj = await _context.Suppliers.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Suppliers.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }



        public async Task<Supplier> DeleteWithName(string name)
        {
            var _obj = await _context.Suppliers.Where(o => o.Name == name).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _context.Suppliers.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Supplier> UpdateAsync(int id, Supplier resource)
        {
            var _obj = await _context.Suppliers.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.Diachi = resource.Diachi;
                _obj.Sdt = resource.Sdt;

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }
}
