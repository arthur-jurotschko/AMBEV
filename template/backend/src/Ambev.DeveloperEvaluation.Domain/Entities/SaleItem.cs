using Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public int Id { get; set; } // Chave primária do SaleItem
    public Guid SaleId { get; set; } // Alterado de int para Guid
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public Sale Sale { get; set; } // Relacionamento com a entidade Sale
}
