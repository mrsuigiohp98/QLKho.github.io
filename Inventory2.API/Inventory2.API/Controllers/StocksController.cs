using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory2.API.Helper;
using Inventory2.API.Models;
using Inventory2.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory2.API.Controllers
{
    [Route("/api/[controller]")]
    public class StocksController : Controller
    {
        private readonly IStockRepository _stockRepository;

        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            var stocks = await _stockRepository.ListAsync();
            return stocks;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<Stock> paged = await _stockRepository.GetAllPagingAsync(pagingParams);

            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);

            return Ok(paged);
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Stock resource)
        {

            var result = await _stockRepository.SaveAsync(resource);


            return Ok(result);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _stockRepository.DeleteAsync(id);


            return Ok(result);
        }




        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Stock resource)
        {
            var result = await _stockRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Stock resource)
        {

            var result = await _stockRepository.UpdateAsync(id, resource);


            return Ok(result);
        }


    }
}