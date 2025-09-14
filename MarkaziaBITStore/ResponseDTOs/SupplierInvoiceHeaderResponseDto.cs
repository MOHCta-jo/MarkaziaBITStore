namespace MarkaziaBITStore.ResponseDTOs
{
    public class SupplierInvoiceHeaderResponseDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int SupplierInvNo { get; set; }
        public DateOnly SupplierInvDate { get; set; }
        public double SupplierInvoiceAmountNet { get; set; }
        public bool? Cancelled { get; set; }

        // Include details
        public List<SupplierInvoiceDetailResponseDto> Details { get; set; } = new();
    }
}
