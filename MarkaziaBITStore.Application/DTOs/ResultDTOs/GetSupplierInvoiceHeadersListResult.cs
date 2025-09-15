using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetSupplierInvoiceHeadersListResult
    {
        public int HeaderID { get; set; }
        public int SupplierID { get; set; }
        public int SupplierInvNo { get; set; }
        public DateOnly SupplierInvDate { get; set; }
        public double SupplierInvoiceAmountNet { get; set; }
        public int? Status { get; set; }
        public int DetailCount { get; set; }
        public double TotalQuantity { get; set; }
    }
}
