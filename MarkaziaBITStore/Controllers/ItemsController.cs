using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using MarkaziaWebCommon.Utils.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Grpc.Core.Metadata;

namespace MarkaziaBITStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly Iitem _itemService;
        private readonly IitemColor _itemColorService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;
        public ItemsController(Iitem itemService, IitemColor itemColorService, ICurrentUserService currentUser)
        {
            _itemService = itemService;
            _itemColorService = itemColorService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetItemsListParam param)
        {
            try
            {
                var pagingResult = await _itemService.GetItemsList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetItemsListResult>
                    {
                        Data = new List<GetItemsListResult>(),
                        Message = "No items found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetItemsListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving items",
                    Error = ex
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _itemService.GetBy(
                x => x.BitItmId == id,
                include: q => q
                .Include(i => i.BitItcItemsColors)
                    .ThenInclude(ic => ic.BitItcBitCol)
                .Include(i => i.BitItcItemsColors)
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
        public async Task<IActionResult> Create([FromBody] ItemRequestDto request)
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
                Status = addedItem.BitItmStatus
            };

            return CreatedAtAction(nameof(GetById), new { id = addedItem.BitItmId }, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemRequestDto request)
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


        [HttpPost("bulk")]
        public async Task<IActionResult> CreateRange([FromBody] List<ItemRequestDto> requests)
        {
            if (requests == null || !requests.Any())
                return BadRequest("No items provided");

            var addedItems = await _itemService.AddRangeAsync(requests, currentUserID);

            
            return CreatedAtAction(nameof(GetAll), addedItems);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateRange([FromBody] List<ItemUpdateRequestDto> requests)
        {
            try
            {
                if (requests == null || !requests.Any())
                    return BadRequest("No items provided");


                await _itemService.EditRangeAsync(requests,currentUserID);


                return Ok("Successfully Updated!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while updating!");
            }
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
