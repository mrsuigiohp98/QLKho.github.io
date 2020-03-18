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
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync();

        Task<PagedList<Customer>> GetAllPagingAsync(PagingParams pagingParams);

        Task<Customer> SaveAsync(Customer customer);

        Task<Customer> DeleteAsync(int id);

        Task<Customer> DeleteWithName(string name);

        Task<Customer> UpdateAsync(int id, Customer resource);
    }
}
