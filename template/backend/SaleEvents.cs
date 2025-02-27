namespace Ambev.DeveloperEvaluation.Core.Events
{
    public static class SaleEvents
    {
        public static void LogSaleCreated(ILogger logger, Sale sale)
        {
            logger.Information($"VendaCriado: Venda {sale.SaleNumber} criada em {sale.SaleDate} para {sale.Customer}.");
        }

        public static void LogSaleModified(ILogger logger, Sale sale)
        {
            logger.Information($"VendaModificado: Venda {sale.SaleNumber} modificada.");
        }

        public static void LogSaleCancelled(ILogger logger, Sale sale)
        {
            logger.Information($"VendaCancelada: Venda {sale.SaleNumber} cancelada.");
        }

        public static void LogItemCancelled(ILogger logger, SaleItem item)
        {
            logger.Information($"ItemCancelado: Item {item.Product} na Venda {item.SaleId} cancelado.");
        }
    }
}
