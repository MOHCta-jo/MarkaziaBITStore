namespace MarkaziaBITStore.RequestDTOs
{
    public class SupplierInvoiceHeaderRequestDto
    {
        public int SupplierInvNo { get; set; }
        public DateOnly SupplierInvDate { get; set; }
        public double SupplierInvoiceAmountNet { get; set; }
    }
}
