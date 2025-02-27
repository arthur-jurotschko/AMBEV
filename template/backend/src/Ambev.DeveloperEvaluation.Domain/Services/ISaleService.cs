using Ambev.DeveloperEvaluation.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface ISaleService
    {
        Task<SaleDTO> GetSaleByIdAsync(int id);
        Task<IEnumerable<SaleDTO>> GetAllSalesAsync();
        Task AddSaleAsync(SaleDTO saleDto);
        Task UpdateSaleAsync(SaleDTO saleDto);
        Task DeleteSaleAsync(int id);
    }

}
