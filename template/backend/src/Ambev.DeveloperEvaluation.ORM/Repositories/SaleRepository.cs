using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> GetSaleByIdAsync(Guid id)
        {
            // Busca uma venda por ID, incluindo itens relacionados
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            // Retorna todas as vendas, incluindo itens relacionados
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync();
        }

        public async Task AddSaleAsync(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "Sale cannot be null.");
            }

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "Sale cannot be null.");
            }

            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale), "Sale not found.");
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
