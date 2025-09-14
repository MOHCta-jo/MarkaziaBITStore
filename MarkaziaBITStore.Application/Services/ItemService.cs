using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services
{
    public class ItemService : GenericService<BitItmItem, BitStoreDbContext>, Iitem
    {
        private readonly IitemColor _itemColorService;

        public ItemService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<ItemService> logger,
            IitemColor itemColorService) : base(bitStoreDbContext, logger)
        {
            _itemColorService = itemColorService;
        }

        private async Task ValidateItemHasColorsAsync(int itemId)
        {
            var hasColors = await _itemColorService
                .GetAllAsQueryable()
                .AnyAsync(c => c.BitItcBitItmid == itemId);

            if (!hasColors)
            {
                throw new InvalidOperationException($"Item with Id {itemId} must have at least one color.");
            }
        }

        public override async Task<BitItmItem> AddAsync(BitItmItem entity)
        {
            using var transaction = await _genDBContext.Database.BeginTransactionAsync();

            try
            {
                var added = await base.AddAsync(entity);

                await ValidateItemHasColorsAsync(added.BitItmId);

                await transaction.CommitAsync();

                return added;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public override async Task EditAsync(BitItmItem entity)
        {
            await ValidateItemHasColorsAsync(entity.BitItmId);

            await base.EditAsync(entity);
        }
    }
}
