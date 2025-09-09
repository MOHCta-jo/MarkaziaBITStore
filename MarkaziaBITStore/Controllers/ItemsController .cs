using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.RequestDTOs;
using MarkaziaBITStore.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkaziaBITStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ItemService _itemService;
        private readonly IitemColor _itemColorService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;
        public ItemsController(ItemService itemService, IitemColor itemColorService, ICurrentUserService currentUser)
        {
            _itemService = itemService;
            _itemColorService = itemColorService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //should using pagination here

            var items =  _itemService.GetAllAsQueryable()
                .Include(i => i.BitItcItemsColors)
                .ThenInclude(ic => ic.BitIciItemsColorImages)
                .ToList();

            if (items == null) return NotFound();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _itemService.GetBy(
                x => x.BitItmId == id,
                include: q => q.Include(i => i.BitItcItemsColors)
                                         .ThenInclude(ic => ic.BitIciItemsColorImages)
                                         .Include(i => i.BitItmBitCat)
            );

            if (entity == null) return NotFound();

            var response = new ItemResponseDto
            {
                Id = entity.BitItmId,
                NameEn = entity.BitItmNameEn,
                NameAr = entity.BitItmNameAr,
                DescriptionEn = entity.BitItmDescriptionEn,
                DescriptionAr = entity.BitItmDescriptionAr,
                Points = entity.BitItmPoints,
                Status = entity.BitItmStatus,
                Category = new CategoryResponseDto
                {
                    Id = entity.BitItmBitCat.BitCatId,
                    NameEn = entity.BitItmBitCat.BitCatNameEn,
                    NameAr = entity.BitItmBitCat.BitCatNameAr,
                    IconUrl = entity.BitItmBitCat.BitCatIconUrl,
                    IsActive = entity.BitItmBitCat.BitCatIsActive
                },
                Colors = entity.BitItcItemsColors.Select(c => new ItemColorResponseDto
                {
                    Id = c.BitItcId,
                    Status = c.BitItcStatus,
                    Color = new ColorResponseDto
                    {
                        Id = c.BitItcBitCol.BitColId,
                        NameEn = c.BitItcBitCol.BitColNameEn,
                        NameAr = c.BitItcBitCol.BitColNameAr,
                        HexCode = c.BitItcBitCol.BitColHexCode
                    },
                    Images = c.BitIciItemsColorImages.Select(img => new ItemColorImageResponseDto
                    {
                        Id = img.BitIciId,
                        ImageUrl = img.BitIciImageUrl,
                        IsDefault = img.BitIciIsDefault
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ItemRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BitItmItem
            {
                BitItmBitCatid = request.CategoryId,
                BitItmNameEn = request.NameEn,
                BitItmNameAr = request.NameAr,
                BitItmDescriptionEn = request.DescriptionEn,
                BitItmDescriptionAr = request.DescriptionAr,
                BitItmPoints = request.Points,
                BitItmStatus = request.Status,
                BitItmBitUsridEnterUser = currentUserID,
                BitItmEnterDate = DateOnly.FromDateTime(DateTime.Now),
                BitItmEnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var addedItem = await _itemService.AddAsync(entity);

            var response = new ItemResponseDto
            {
                Id = addedItem.BitItmId,
                NameEn = addedItem.BitItmNameEn,
                NameAr = addedItem.BitItmNameAr,
                DescriptionEn = addedItem.BitItmDescriptionEn,
                DescriptionAr = addedItem.BitItmDescriptionAr,
                Points = addedItem.BitItmPoints,
                Status = addedItem.BitItmStatus,
                Category = new CategoryResponseDto
                {
                    Id = addedItem.BitItmBitCat.BitCatId,
                    NameEn = addedItem.BitItmBitCat.BitCatNameEn,
                    NameAr = addedItem.BitItmBitCat.BitCatNameAr,
                    IconUrl = addedItem.BitItmBitCat.BitCatIconUrl,
                    IsActive = addedItem.BitItmBitCat.BitCatIsActive
                },
                Colors = new List<ItemColorResponseDto>()
            };

            return CreatedAtAction(nameof(GetById), new { id = addedItem.BitItmId }, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemRequestDto request)
        {
            var entity = await _itemService.GetBy(x => x.BitItmId == id);
            if (entity == null) return NotFound();

            entity.BitItmBitCatid = request.CategoryId;
            entity.BitItmNameEn = request.NameEn;
            entity.BitItmNameAr = request.NameAr;
            entity.BitItmDescriptionEn = request.DescriptionEn;
            entity.BitItmDescriptionAr = request.DescriptionAr;
            entity.BitItmPoints = request.Points;
            entity.BitItmStatus = request.Status;
            entity.BitItmBitUsridModUser = currentUserID;
            entity.BitItmModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitItmModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemService.EditAsync(entity);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _itemService.GetBy(x => x.BitItmId == id);
            if (entity == null) return NotFound();

            entity.BitItmCancelled = true;
            entity.BitItmBitUsridCancelledUser = currentUserID;
            entity.BitItmCancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitItmCancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/colors")]
        public async Task<IActionResult> GetColorsOfItem(int id)
        {
            var colors = await _itemService.GetBy(x => x.BitItmId == id, true, false, x => x.Include(x => x.BitItcItemsColors))
                ;

            if (colors.BitItcItemsColors == null) return NotFound();

            return Ok(colors.BitItcItemsColors);
        }
    }
}
