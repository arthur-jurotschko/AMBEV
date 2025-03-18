using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Commands.Handlers
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Criar a entidade Sale
            var sale = new Sale
            {
                SaleNumber = request.SaleNumber,
                TotalAmount = request.TotalAmount,
                SaleDate = request.SaleDate,
                Customer = request.Customer,
                Branch = request.Branch
            };

            // Salvar a venda no repositório
            await _saleRepository.AddSaleAsync(sale);

            return sale.Id;
        }
    }
}
