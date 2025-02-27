using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Core.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddSaleAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.AddSaleAsync(sale);
            SaleEvents.LogSaleCreated(_logger, sale); // Log do evento VendaCriado
        }

        public async Task UpdateSaleAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _saleRepository.UpdateSaleAsync(sale);
            SaleEvents.LogSaleModified(_logger, sale); // Log do evento VendaModificado
        }

        public async Task DeleteSaleAsync(int id)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(id);
            if (sale != null)
            {
                await _saleRepository.DeleteSaleAsync(id);
                SaleEvents.LogSaleCancelled(_logger, sale); // Log do evento VendaCancelada
            }
        }

        public async Task CancelSaleItemAsync(int saleId, int itemId)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId);
            var item = sale.Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                sale.Items.Remove(item);
                await _saleRepository.UpdateSaleAsync(sale);
                SaleEvents.LogItemCancelled(_logger, item); // Log do evento ItemCancelado
            }
        }
    }
}
