using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services
{
    public class SupplierInvoiceDetailService : GenericService<BitSidSupplierInvoiceDetail, BitStoreDbContext>, ISupplierInvoiceDetails
    {
        public SupplierInvoiceDetailService(BitStoreDbContext bitStoreDbContext,
            ILogger<GenericService<BitSidSupplierInvoiceDetail, BitStoreDbContext>> logger) : base(bitStoreDbContext, logger)
        {
        }
    }
}
