using FluentAssertions;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Unit
{
    public class SaleTests
    {
        [Fact]
        public void Should_Have_Valid_TotalAmount_When_Created()
        {
            // Arrange
            var sale = new Sale
            {
                SaleNumber = "SALE123",
                TotalAmount = 100,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                Branch = "Main Branch",
                IsCancelled = false
            };

            // Act & Assert
            sale.TotalAmount.Should().BeGreaterThan(0, "the total amount must be greater than zero.");
        }

        [Fact]
        public void Should_Fail_When_TotalAmount_Is_Negative()
        {
            // Arrange
            var sale = new Sale
            {
                SaleNumber = "SALE123",
                TotalAmount = -50,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                Branch = "Main Branch",
                IsCancelled = false
            };

            // Act & Assert
            sale.TotalAmount.Should().BeNegative("negative values are not allowed for TotalAmount.");
        }
    }
}
