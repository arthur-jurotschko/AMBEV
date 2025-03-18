using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Tests.Integration
{
    public class SaleRepositoryTests
    {
        private readonly DbContextOptions<DefaultContext> _dbContextOptions;

        public SaleRepositoryTests()
        {
            // Configurando o DbContext para uso com InMemoryDatabase
            _dbContextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddSaleAsync_ShouldAddSale()
        {
            // Arrange
            using var context = new DefaultContext(_dbContextOptions);
            var repository = new SaleRepository(context);
            
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "SALE001",
                TotalAmount = 100,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                Branch = "Main Branch"
            };

            // Act
            await repository.AddSaleAsync(sale);

            // Assert
            var savedSale = await context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == "SALE001");
            Assert.NotNull(savedSale);
            Assert.Equal("SALE001", savedSale.SaleNumber);
        }

        [Fact]
        public async Task GetAllSalesAsync_ShouldReturnAllSales()
        {
            // Arrange
            using var context = new DefaultContext(_dbContextOptions);
            var repository = new SaleRepository(context);

            var sales = new[]
            {
                new Sale { Id = Guid.NewGuid(), SaleNumber = "S001", TotalAmount = 100, SaleDate = DateTime.UtcNow, Customer = "Customer A", Branch = "Branch A" },
                new Sale { Id = Guid.NewGuid(), SaleNumber = "S002", TotalAmount = 200, SaleDate = DateTime.UtcNow, Customer = "Customer B", Branch = "Branch B" }
            };

            await context.Sales.AddRangeAsync(sales);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllSalesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetSaleByIdAsync_ShouldReturnCorrectSale()
        {
            // Arrange
            using var context = new DefaultContext(_dbContextOptions);
            var repository = new SaleRepository(context);

            var saleId = Guid.NewGuid();
            var sale = new Sale
            {
                Id = saleId,
                SaleNumber = "S003",
                TotalAmount = 300,
                SaleDate = DateTime.UtcNow,
                Customer = "Customer C",
                Branch = "Branch C"
            };

            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetSaleByIdAsync(saleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(saleId, result.Id);
        }
    }
}
