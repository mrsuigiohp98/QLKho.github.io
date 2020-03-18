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
    public class ReceiptRepository : BaseRepository, IReceiptRepository
    {
        public ReceiptRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Receipt>> ListAsync()
        {
            return await _context.Receipts.ToListAsync();
        }

        public async Task<PagedList<ReceiptViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<ReceiptViewModel> _query = from u in _context.Receipts
                                         join u2 in _context.Inventories on u.InventoryId equals u2.Id
                                         join u3 in _context.Suppliers on u.SupplierId equals u3.Id

                                         orderby u.Name
                                         select new ReceiptViewModel 
                                         { 
                                             Id = u.Id, 
                                             Name = u.Name, 
                                             SupplierId = u3.Id,
                                             SupplierName = u3.Name,
                                             Ngaynhap = u.Ngaynhap,
                                             InventoryId = u2.Id,
                                             InventoryName = u2.Name,
                                             Soluong = u.Soluong, 
                                             Dongia = u.Dongia, 
                                             Thanhtien = u.Thanhtien 
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

            return await PagedList<ReceiptViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Receipt> SaveAsync(Receipt _obj)
        {
            await _context.Receipts.AddAsync(_obj);
            await _context.SaveChangesAsync();

            return _obj;

        }


        public async Task<Receipt> DeleteAsync(int id)
        {
            var _obj = await _context.Receipts.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Receipts.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }



        public async Task<Receipt> DeleteWithName(string name)
        {
            var _obj = await _context.Receipts.Where(o => o.Name == name).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _context.Receipts.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Receipt> UpdateAsync(int id, Receipt resource)
        {
            var _obj = await _context.Receipts.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.SupplierId = resource.SupplierId;
                _obj.Ngaynhap = resource.Ngaynhap;
                _obj.InventoryId = resource.InventoryId;
                _obj.Soluong = resource.Soluong;
                _obj.Dongia = resource.Dongia;
                _obj.Thanhtien = resource.Thanhtien;

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
    }
}
