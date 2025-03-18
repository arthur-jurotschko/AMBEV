using Xunit;
using Moq;
using AutoMapper;
using Bogus;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Services;
using Ambev.DeveloperEvaluation.WebApi.Mappings;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Tests.Unit
{
    public class SaleServiceTests
    {
        private readonly SaleService _saleService;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Faker<Sale> _saleFaker;

        public SaleServiceTests()
        {
            // Configurar o mock do repositório
            _saleRepositoryMock = new Mock<ISaleRepository>();

            // Configurar o AutoMapper
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = configuration.CreateMapper();

            // Configurar o faker para gerar dados fake
            _saleFaker = new Faker<Sale>()
                .RuleFor(s => s.Id, f => f.Random.Guid())
                .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(s => s.Customer, f => f.Person.FullName)
                .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(10, 1000));

            // Inicializar o serviço com dependências fake
            _saleService = new SaleService(_saleRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllSalesAsync_ShouldReturnFakeSales()
        {
            // Arrange: Gerar uma lista fake de vendas
            var fakeSales = _saleFaker.Generate(5);

            _saleRepositoryMock
                .Setup(repo => repo.GetAllSalesAsync())
                .ReturnsAsync(fakeSales);

            // Act: Recuperar as vendas pelo serviço
            var result = await _saleService.GetAllSalesAsync();

            // Assert: Verificar o resultado
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
            result.First().Customer.Should().NotBeNullOrEmpty();
        }
    }
}
