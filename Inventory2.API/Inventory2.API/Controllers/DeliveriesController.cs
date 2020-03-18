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
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveriesController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            var deliveries = await _deliveryRepository.ListAsync();
            return deliveries;
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            PagedList<DeliveryViewModel> paged = await _deliveryRepository.GetAllPagingAsync(pagingParams);

            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);

            return Ok(paged);
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Delivery resource)
        {

            var result = await _deliveryRepository.SaveAsync(resource);


            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _deliveryRepository.DeleteAsync(id);


            return Ok(result);
        }

        [HttpDelete("DeleteWithName")]
        public async Task<IActionResult> DeleteWithName([FromBody] Delivery resource)
        {
            var result = await _deliveryRepository.DeleteWithName(resource.Name);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Delivery resource)
        {

            var result = await _deliveryRepository.UpdateAsync(id, resource);


            return Ok(result);
        }
    }
}