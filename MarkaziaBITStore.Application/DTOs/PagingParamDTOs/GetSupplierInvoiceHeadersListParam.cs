using MarkaziaWebCommon.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.DTOs.PagingParamDTOs
{

    public class GetSupplierInvoiceHeadersListParam : PagingParam
    {
        [SwaggerParameter("""
        (ASC, DESC)
        Default 1 
        HeaderID (2,3)
        SupplierID (4,5)
        SupplierInvNo (6,7)
        SupplierInvDate (8,9)
        NetAmount (10,11)
        Status (12,13)
       """)]
        public int Sort { get; set; } = 1;
        public string? TextToSearch { get; set; }
        public int? HeaderID { get; set; }
        public int? SupplierID { get; set; }
        public int? SupplierInvNo { get; set; }
        public DateOnly? SupplierInvDate { get; set; }
        public double? NetAmount { get; set; }
        public int? Status { get; set; }
    }
}
