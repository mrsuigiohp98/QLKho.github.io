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
    public class DeliveryRepository : BaseRepository , IDeliveryRepository
    {
        public DeliveryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Delivery>> ListAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task<PagedList<DeliveryViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<DeliveryViewModel> _query = from u in _context.Deliveries
                                         join u2 in _context.Inventories on u.InventoryId equals u2.Id
                                         join u3 in _context.Customers on u.CustomerId equals u3.Id

                                         orderby u.Name
                                         select new DeliveryViewModel
                                         {
                                             Id = u.Id,
                                             Name = u.Name,
                                             CustomerId = u3.Id,
                                             CustomerName = u3.Name,
                                             Ngayxuat = u.Ngayxuat,
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

            return await PagedList<DeliveryViewModel>
                .CreateAsync(_query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<Delivery> SaveAsync(Delivery _obj)
        {
            await _context.Deliveries.AddAsync(_obj);
            await _context.SaveChangesAsync();

            return _obj;

        }


        public async Task<Delivery> DeleteAsync(int id)
        {
            var _obj = await _context.Deliveries.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (_obj != null)
            {
                _context.Deliveries.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }



        public async Task<Delivery> DeleteWithName(string name)
        {
            var _obj = await _context.Deliveries.Where(o => o.Name == name).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _context.Deliveries.Remove(_obj);

                await _context.SaveChangesAsync();
            }
            return _obj;
        }
        public async Task<Delivery> UpdateAsync(int id, Delivery resource)
        {
            var _obj = await _context.Deliveries.Where(o => o.Id == id).FirstOrDefaultAsync();

            if (_obj != null)
            {
                _obj.Name = resource.Name;
                _obj.CustomerId = resource.CustomerId;
                _obj.Ngayxuat = resource.Ngayxuat;
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
