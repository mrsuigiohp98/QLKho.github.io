using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory2.API.Helper;
using Inventory2.API.Models;
using Inventory2.API.Repositories;
using Inventory2.API.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : Controller
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptsController(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            var receipts = await _receiptRepository.ListAsync();
            return receipts;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<ReceiptViewModel> paged = await _receiptRepository.GetAllPagingAsync(pagingParams);

            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);

            return Ok(paged);
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Receipt resource)
        {

            var result = await _receiptRepository.SaveAsync(resource);


            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _receiptRepository.DeleteAsync(id);


            return Ok(result);
        }

        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Receipt resource)
        {
            var result = await _receiptRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Receipt resource)
        {

            var result = await _receiptRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}