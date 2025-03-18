using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
 
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task AddSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task<Sale> GetSaleByIdAsync(Guid id);
        Task DeleteSaleAsync(Guid id);

    }

}
