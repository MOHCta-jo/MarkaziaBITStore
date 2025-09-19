using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using MarkaziaWebCommon.Utils.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                x => x.BIT_ITM_ID == id,
                include: q => q
                .Include(i => i.BIT_ITC_ItemsColor)
                    .ThenInclude(ic => ic.BIT_ITC__BIT_COL)
                .Include(i => i.BIT_ITC_ItemsColor)
                    .ThenInclude(ic => ic.BIT_ICI_ItemsColorImages)
                .Include(i => i.BIT_ITM__BIT_CAT)
            );

            if (entity == null) return NotFound();

            var response = new ItemResponseDto
            {
                Id = entity.BIT_ITM_ID,
                NameEn = entity.BIT_ITM_NameEN,
                NameAr = entity.BIT_ITM_NameAR,
                DescriptionEn = entity.BIT_ITM_DescriptionEN,
                DescriptionAr = entity.BIT_ITM_DescriptionAR,
                Points = entity.BIT_ITM_Points,
                Status = entity.BIT_ITM_Status,
                Category = new CategoryResponseDto
                {
                    Id = entity.BIT_ITM__BIT_CAT.BIT_CAT_ID,
                    NameEn = entity.BIT_ITM__BIT_CAT.BIT_CAT_NameEN,
                    NameAr = entity.BIT_ITM__BIT_CAT.BIT_CAT_NameAR,
                    IconUrl = entity.BIT_ITM__BIT_CAT.BIT_CAT_IconURL,
                    IsActive = entity.BIT_ITM__BIT_CAT.BIT_CAT_IsActive
                },
                Colors = entity.BIT_ITC_ItemsColor.Select(c => new ItemColorResponseDto
                {
                    Id = c.BIT_ITC_ID,
                    Status = c.BIT_ITC_Status,
                    Color = new ColorResponseDto
                    {
                        Id = c.BIT_ITC__BIT_COL.BIT_COL_ID,
                        NameEn = c.BIT_ITC__BIT_COL.BIT_COL_NameEN,
                        NameAr = c.BIT_ITC__BIT_COL.BIT_COL_NameAR,
                        HexCode = c.BIT_ITC__BIT_COL.BIT_COL_HexCode
                    },
                    Images = c.BIT_ICI_ItemsColorImages.Select(img => new ItemColorImageResponseDto
                    {
                        Id = img.BIT_ICI_ID,
                        ImageUrl = img.BIT_ICI_ImageURL,
                        IsDefault = img.BIT_ICI_IsDefault
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BIT_ITM_Items
            {
                BIT_ITM__BIT_CATID = request.CategoryId,
                BIT_ITM_NameEN = request.NameEn,
                BIT_ITM_NameAR = request.NameAr,
                BIT_ITM_DescriptionEN = request.DescriptionEn,
                BIT_ITM_DescriptionAR = request.DescriptionAr,
                BIT_ITM_Points = request.Points,
                BIT_ITM_Status = request.Status,
                BIT_ITM__MAS_USRID_EnterUser = currentUserID,
                BIT_ITM_EnterDate = DateOnly.FromDateTime(DateTime.Now),
                BIT_ITM_EnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var addedItem = await _itemService.AddAsync(entity,request.Colors);


            return Ok("Added Successfully!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemRequestDto request)
        {
            var entity = await _itemService.GetBy(x => x.BIT_ITM_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ITM__BIT_CATID = request.CategoryId;
            entity.BIT_ITM_NameEN = request.NameEn;
            entity.BIT_ITM_NameAR = request.NameAr;
            entity.BIT_ITM_DescriptionEN = request.DescriptionEn;
            entity.BIT_ITM_DescriptionAR = request.DescriptionAr;
            entity.BIT_ITM_Points = request.Points;
            entity.BIT_ITM_Status = request.Status;
            entity.BIT_ITM__MAS_USRID_ModUser = currentUserID;
            entity.BIT_ITM_ModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ITM_ModTime = TimeOnly.FromDateTime(DateTime.Now);

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
            var entity = await _itemService.GetBy(x => x.BIT_ITM_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ITM_Cancelled = true;
            entity.BIT_ITM__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_ITM_CancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ITM_CancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/colors")]
        public async Task<IActionResult> GetColorsOfItem(int id)
        {
            var colors = await _itemService.GetBy(x => x.BIT_ITM_ID == id, true, false, x => x.Include(x => x.BIT_ITC_ItemsColor))
                ;

            if (colors.BIT_ITC_ItemsColor == null) return NotFound();

            return Ok(colors.BIT_ITC_ItemsColor);
        }
    }
}
