using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> GetSaleByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(id);
            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<IEnumerable<SaleDTO>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllSalesAsync();
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task AddSaleAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.AddSaleAsync(sale);
        }

        public async Task UpdateSaleAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            await _saleRepository.DeleteSaleAsync(id);
        }
    }

}
