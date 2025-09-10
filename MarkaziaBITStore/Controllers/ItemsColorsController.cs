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
    public class ItemsColorsController : Controller
    {
        private readonly IitemColor _itemColorService;
        private readonly IitemsColorImage _itemColorImageService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;

        public ItemsColorsController(IitemColor itemColorService, ICurrentUserService currentUser, 
            IitemsColorImage itemColorImageService)
        {
            _itemColorService = itemColorService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
            _itemColorImageService = itemColorImageService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            // should use paggination here
            var list = await _itemColorService.GetAllAsQueryable()
                .Include(ic => ic.BitItcBitItm)
                .Include(ic => ic.BitItcBitCol)
                .Include(ic => ic.BitIciItemsColorImages)
                .ToListAsync();

            var result =  list.Select(ic => new ItemColorResponseDto
            {
                Id = ic.BitItcId,
                Status = ic.BitItcStatus,
                Item = new ItemResponseDto
                {
                    Id = ic.BitItcBitItm.BitItmId,
                    NameEn = ic.BitItcBitItm.BitItmNameEn,
                    NameAr = ic.BitItcBitItm.BitItmNameAr,
                    DescriptionEn = ic.BitItcBitItm.BitItmDescriptionEn,
                    DescriptionAr = ic.BitItcBitItm.BitItmDescriptionAr,
                    Points = ic.BitItcBitItm.BitItmPoints,
                    Status = ic.BitItcBitItm.BitItmStatus
                },
                Color = new ColorResponseDto
                {
                    Id = ic.BitItcBitCol.BitColId,
                    NameEn = ic.BitItcBitCol.BitColNameEn,
                    NameAr = ic.BitItcBitCol.BitColNameAr,
                    HexCode = ic.BitItcBitCol.BitColHexCode
                },
                Images = ic.BitIciItemsColorImages.Select(img => new ItemColorImageResponseDto
                {
                    Id = img.BitIciId,
                    ImageUrl = img.BitIciImageUrl,
                    IsDefault = img.BitIciIsDefault,
                    Sequence = img.BitIciSequence
                }).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ic = await _itemColorService.GetBy(
                x => x.BitItcId == id,
                include: q => q.Include(i => i.BitItcBitItm)
                               .Include(i => i.BitItcBitCol)
                               .Include(i => i.BitIciItemsColorImages)
            );

            if (ic == null) return NotFound();

            var response = new ItemColorResponseDto
            {
                Id = ic.BitItcId,
                Status = ic.BitItcStatus,
                Item = new ItemResponseDto
                {
                    Id = ic.BitItcBitItm.BitItmId,
                    NameEn = ic.BitItcBitItm.BitItmNameEn,
                    NameAr = ic.BitItcBitItm.BitItmNameAr,
                    DescriptionEn = ic.BitItcBitItm.BitItmDescriptionEn,
                    DescriptionAr = ic.BitItcBitItm.BitItmDescriptionAr,
                    Points = ic.BitItcBitItm.BitItmPoints,
                    Status = ic.BitItcBitItm.BitItmStatus
                },
                Color = new ColorResponseDto
                {
                    Id = ic.BitItcBitCol.BitColId,
                    NameEn = ic.BitItcBitCol.BitColNameEn,
                    NameAr = ic.BitItcBitCol.BitColNameAr,
                    HexCode = ic.BitItcBitCol.BitColHexCode
                },
                Images = ic.BitIciItemsColorImages.Select(img => new ItemColorImageResponseDto
                {
                    Id = img.BitIciId,
                    ImageUrl = img.BitIciImageUrl,
                    IsDefault = img.BitIciIsDefault,
                    Sequence = img.BitIciSequence
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ItemColorRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BitItcItemsColor
            {
                BitItcBitItmid = request.ItemId,
                BitItcBitColid = request.ColorId,
                BitItcStatus = request.Status,
                BitItcBitUsridEnterUser = currentUserID,
                BitItcEnterDate = DateOnly.FromDateTime(DateTime.Now),
                BitItcEnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var added = await _itemColorService.AddAsync(entity);

            var response = new ItemColorResponseDto
            {
                Id = added.BitItcId,
                Status = added.BitItcStatus
            };

            return CreatedAtAction(nameof(GetById), new { id = added.BitItcId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemColorRequestDto request)
        {
            var entity = await _itemColorService.GetBy(x => x.BitItcId == id);
            if (entity == null) return NotFound();

            entity.BitItcBitItmid = request.ItemId;
            entity.BitItcBitColid = request.ColorId;
            entity.BitItcStatus = request.Status;
            entity.BitItcBitUsridModUser = currentUserID;
            entity.BitItcModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitItcModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorService.EditAsync(entity);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _itemColorService.GetBy(x => x.BitItcId == id);
            if (entity == null) return NotFound();

            entity.BitItcCancelled = true;
            entity.BitItcBitUsridCancelledUser = currentUserID;
            entity.BitItcCancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitItcCancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImagesByItemColor(int id)
        {
            var ic = await _itemColorService.GetBy(
                x => x.BitItcId == id,
                include: q => q.Include(x => x.BitIciItemsColorImages)
            );

            if (ic?.BitIciItemsColorImages == null || !ic.BitIciItemsColorImages.Any())
                return NotFound();

            var images = ic.BitIciItemsColorImages.Select(img => new ItemColorImageResponseDto
            {
                Id = img.BitIciId,
                ImageUrl = img.BitIciImageUrl,
                IsDefault = img.BitIciIsDefault,
                Sequence = img.BitIciSequence
            }).ToList();

            return Ok(images);
        }

    }
}
