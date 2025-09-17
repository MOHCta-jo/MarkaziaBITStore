using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
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
        public async Task<IActionResult> GetAll([FromQuery] GetItemColorsListParam param)
        {
            try
            {
                var pagingResult = await _itemColorService.GetItemColorsList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetItemColorsListResult>
                    {
                        Data = new List<GetItemColorsListResult>(),
                        Message = "No item colors found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetItemColorsListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving item colors",
                    Error = ex
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ic = await _itemColorService.GetBy(
                x => x.BIT_ITC_ID == id,
                include: q => q.Include(i => i.BIT_ITC__BIT_ITM)
                               .Include(i => i.BIT_ITC__BIT_COL)
                               .Include(i => i.BIT_ICI_ItemsColorImages)
            );

            if (ic == null) return NotFound();

            var response = new ItemColorResponseDto
            {
                Id = ic.BIT_ITC_ID,
                Status = ic.BIT_ITC_Status,
                Item = new ItemResponseDto
                {
                    Id = ic.BIT_ITC__BIT_ITM.BIT_ITM_ID,
                    NameEn = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameEN,
                    NameAr = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameAR,
                    DescriptionEn = ic.BIT_ITC__BIT_ITM.BIT_ITM_DescriptionEN,
                    DescriptionAr = ic.BIT_ITC__BIT_ITM.BIT_ITM_DescriptionAR,
                    Points = ic.BIT_ITC__BIT_ITM.BIT_ITM_Points,
                    Status = ic.BIT_ITC__BIT_ITM.BIT_ITM_Status
                },
                Color = new ColorResponseDto
                {
                    Id = ic.BIT_ITC__BIT_COL.BIT_COL_ID,
                    NameEn = ic.BIT_ITC__BIT_COL.BIT_COL_NameEN,
                    NameAr = ic.BIT_ITC__BIT_COL.BIT_COL_NameAR,
                    HexCode = ic.BIT_ITC__BIT_COL.BIT_COL_HexCode
                },
                Images = ic.BIT_ICI_ItemsColorImages.Select(img => new ItemColorImageResponseDto
                {
                    Id = img.BIT_ICI_ID,
                    ImageUrl = img.BIT_ICI_ImageURL,
                    IsDefault = img.BIT_ICI_IsDefault,
                    Sequence = img.BIT_ICI_ScreenSequence
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemColorRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BIT_ITC_ItemsColor
            {
                BIT_ITC__BIT_ITMID = request.ItemId,
                BIT_ITC__BIT_COLID = request.ColorId,
                BIT_ITC_Status = request.Status,
                BIT_ITC__MAS_USRID_EnterUser = currentUserID,
                BIT_ITC_EnterDate = DateOnly.FromDateTime(DateTime.Now),
                BIT_ITC_EnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var added = await _itemColorService.AddAsync(entity);

            var response = new ItemColorResponseDto
            {
                Id = added.BIT_ITC_ID,
                Status = added.BIT_ITC_Status
            };

            return CreatedAtAction(nameof(GetById), new { id = added.BIT_ITC_ID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemColorRequestDto request)
        {
            var entity = await _itemColorService.GetBy(x => x.BIT_ITC_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ITC__BIT_ITMID = request.ItemId;
            entity.BIT_ITC__BIT_COLID = request.ColorId;
            entity.BIT_ITC_Status = request.Status;
            entity.BIT_ITC__MAS_USRID_ModUser = currentUserID;
            entity.BIT_ITC_ModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ITC_ModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorService.EditAsync(entity);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _itemColorService.GetBy(x => x.BIT_ITC_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ITC_Cancelled = true;
            entity.BIT_ITC__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_ITC_CancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ITC_CancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImagesByItemColor(int id)
        {
            var ic = await _itemColorService.GetBy(
                x => x.BIT_ITC_ID == id,
                include: q => q.Include(x => x.BIT_ICI_ItemsColorImages)
            );

            if (ic?.BIT_ICI_ItemsColorImages == null || !ic.BIT_ICI_ItemsColorImages.Any())
                return NotFound();

            var images = ic.BIT_ICI_ItemsColorImages.Select(img => new ItemColorImageResponseDto
            {
                Id = img.BIT_ICI_ID,
                ImageUrl = img.BIT_ICI_ImageURL,
                IsDefault = img.BIT_ICI_IsDefault,
                Sequence = img.BIT_ICI_ScreenSequence
            }).ToList();

            return Ok(images);
        }

    }
}
