namespace MarkaziaBITStore.Application.DTOs.ResponseDTOs
{
    public class SupplierInvoiceDetailResponseDto
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int ItemColorId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public bool? Cancelled { get; set; }
    }
}
