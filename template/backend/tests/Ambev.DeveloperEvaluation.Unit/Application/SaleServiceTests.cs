using Xunit;
using Moq;

using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Services;
using Ambev.DeveloperEvaluation.WebApi.Mappings;

namespace Ambev.DeveloperEvaluation.Tests.Unit
{
    public class SaleServiceTests
    {
        private readonly SaleService _saleService;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IMapper _mapper;

        public SaleServiceTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = configuration.CreateMapper();
            _saleService = new SaleService(_saleRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task AddSaleAsync_ShouldAddSale()
        {
            // Arrange
            var saleDto = new SaleDTO { Id = 1, SaleNumber = "S001", Customer = "Cliente A" };
            var sale = _mapper.Map<Sale>(saleDto);
            _saleRepositoryMock.Setup(repo => repo.AddSaleAsync(It.IsAny<Sale>())).Returns(Task.CompletedTask);

            // Act
            await _saleService.AddSaleAsync(saleDto);

            // Assert
            _saleRepositoryMock.Verify(repo => repo.AddSaleAsync(It.Is<Sale>(s => s.SaleNumber == sale.SaleNumber)), Times.Once);
        }


        [Fact]
        public async Task GetAllSalesAsync_ShouldReturnSales()
        {
            // Arrange
            var sales = new List<Sale> { new Sale { Id = 1, SaleNumber = "S001", Customer = "Cliente A" } };
            _saleRepositoryMock.Setup(repo => repo.GetAllSalesAsync()).ReturnsAsync(sales);

            // Act
            var result = await _saleService.GetAllSalesAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("S001", result.First().SaleNumber);
        }
    }
}