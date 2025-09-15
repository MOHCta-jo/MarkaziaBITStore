using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{
    public class GetSupplierInvoiceDetailsListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        DetailID (2,3)
        HeaderID (4,5)
        ItemColorID (6,7)
        Quantity (8,9)
        UnitPrice (10,11)
        Status (12,13)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? DetailID { get; set; }
        public int? HeaderID { get; set; }
        public int? ItemColorID { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
        public int? Status { get; set; }
    }
}
