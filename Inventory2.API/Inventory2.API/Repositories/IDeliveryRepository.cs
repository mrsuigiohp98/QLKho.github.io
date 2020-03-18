using Inventory2.API.Helper;
using Inventory2.API.Models;
using Inventory2.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Repositories
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> ListAsync();

        Task<PagedList<DeliveryViewModel>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Delivery> SaveAsync(Delivery receipt);

        Task<Delivery> DeleteAsync(int id);

        Task<Delivery> DeleteWithName(string name);

        Task<Delivery> UpdateAsync(int id, Delivery resource);
    }
}
