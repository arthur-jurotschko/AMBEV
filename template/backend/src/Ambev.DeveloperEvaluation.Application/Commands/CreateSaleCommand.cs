using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Commands
{
    public class CreateSaleCommand : IRequest<Guid>
    {
        public string SaleNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string Customer { get; set; }
        public DateTime SaleDate { get; set; }
        public string Branch { get; set; }
    }
}
