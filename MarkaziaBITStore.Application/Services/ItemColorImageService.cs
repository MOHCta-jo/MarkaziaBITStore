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
    public class ItemColorImageService : GenericService<BitIciItemsColorImage, BitStoreDbContext>, IitemsColorImage
    {
        public ItemColorImageService(BitStoreDbContext bitStoreDbContext, ILogger<ItemColorImageService> logger) : base(bitStoreDbContext, logger)
        {
        }
    }
}
