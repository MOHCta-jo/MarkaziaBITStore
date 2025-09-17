using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkaziaBITStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorsController : Controller
    {
        private readonly IColor _colorService;
        private readonly IitemColor _IitemColorService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;

        public ColorsController(IColor colorService, IitemColor iitemColorService, 
            ICurrentUserService currentUser)
        {
            _colorService = colorService;
            _IitemColorService = iitemColorService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetColorsListParam param)
        {
            try
            {
                var pagingResult = await _colorService.GetColorsList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetColorsListResult>
                    {
                        Data = new List<GetColorsListResult>(),
                        Message = "No colors found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetColorsListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving colors",
                    Error = ex
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _colorService.GetBy(
                x => x.BIT_COL_ID == id,
                include: q => q.Include(c => c.BIT_ITC_ItemsColor)
                                         .ThenInclude(ic => ic.BIT_ITC__BIT_ITM)
            );

            if (entity == null) return NotFound();

            var response = new ColorResponseDto
            {
                Id = entity.BIT_COL_ID,
                NameEn = entity.BIT_COL_NameEN,
                NameAr = entity.BIT_COL_NameAR,
                HexCode = entity.BIT_COL_HexCode,
                ItemsColors = entity.BIT_ITC_ItemsColor.Select(ic => new ItemColorResponseDto
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
                    }
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColorRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BIT_COL_Colors
            {
                BIT_COL_NameEN = request.NameEn,
                BIT_COL_NameAR = request.NameAr,
                BIT_COL_HexCode = request.HexCode,
                BIT_COL__MAS_USRID_EnterUser = currentUserID,
                BIT_COL_EnterDate = DateOnly.FromDateTime(DateTime.Now),
                BIT_COL_EnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var addedColor = await _colorService.AddAsync(entity);

            var response = new ColorResponseDto
            {
                Id = addedColor.BIT_COL_ID,
                NameEn = addedColor.BIT_COL_NameEN,
                NameAr = addedColor.BIT_COL_NameAR,
                HexCode = addedColor.BIT_COL_HexCode
            };

            return CreatedAtAction(nameof(GetById), new { id = addedColor.BIT_COL_ID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ColorRequestDto request)
        {
            var entity = await _colorService.GetBy(x => x.BIT_COL_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_COL_NameEN = request.NameEn;
            entity.BIT_COL_NameAR = request.NameAr;
            entity.BIT_COL_HexCode = request.HexCode;
            entity.BIT_COL__MAS_USRID_ModUser = currentUserID;
            entity.BIT_COL_ModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_COL_ModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _colorService.EditAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _colorService.GetBy(x => x.BIT_COL_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_COL_Cancelled = true;
            entity.BIT_COL__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_COL_CancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_COL_CancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _colorService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsByColor(int id)
        {
            var itemsColors = await _IitemColorService.GetByListAsync(
                x => x.BIT_ITC__BIT_COLID == id,
                includeProperties : x => x.BIT_ITC__BIT_ITM
            );

            var itemsList = itemsColors.Select(ic => new ItemResponseDto
            {
                Id = ic.BIT_ITC__BIT_ITM.BIT_ITM_ID,
                NameEn = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameEN,
                NameAr = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameAR,
                DescriptionEn = ic.BIT_ITC__BIT_ITM.BIT_ITM_DescriptionEN,
                DescriptionAr = ic.BIT_ITC__BIT_ITM.BIT_ITM_DescriptionAR,
                Points = ic.BIT_ITC__BIT_ITM.BIT_ITM_Points,
                Status = ic.BIT_ITC__BIT_ITM.BIT_ITM_Status
            }).ToList();

            if (!itemsList.Any()) return NotFound();

            return Ok(itemsList);
        }

    }
}
