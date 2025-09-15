using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.ResultDTOs
{
    public class GetSupplierInvoiceDetailsListResult
    {
        public int DetailID { get; set; }
        public int HeaderID { get; set; }
        public int ItemColorID { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public int? Status { get; set; }
        public double? TotalPrice { get; set; }
    }
}
