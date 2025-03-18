using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(Guid id)
        {
            // Lógica para recuperar a venda pelo ID
            var sale = new SaleDTO
            {
                Id = id,
                SaleNumber = "SALE12345",
                TotalAmount = 250.0m,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                Items = new List<SaleItemDTO>
            {
                new SaleItemDTO { Product = "Product A", Quantity = 2, UnitPrice = 50.0m, Discount = 0.0m },
                new SaleItemDTO { Product = "Product B", Quantity = 1, UnitPrice = 150.0m, Discount = 0.0m }
            }
            };

            return Ok(sale);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSale(Guid id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSale(SaleDTO saleDto)
        {
            await _saleService.AddSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetSale), new { id = saleDto.Id }, saleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, SaleDTO saleDto)
        {
            if (id != saleDto.Id)
            {
                return BadRequest();
            }

            await _saleService.UpdateSaleAsync(saleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
    }
}
