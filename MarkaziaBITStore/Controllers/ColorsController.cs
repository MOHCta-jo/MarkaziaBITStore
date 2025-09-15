using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
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
                x => x.BitColId == id,
                include: q => q.Include(c => c.BitItcItemsColors)
                                         .ThenInclude(ic => ic.BitItcBitItm)
            );

            if (entity == null) return NotFound();

            var response = new ColorResponseDto
            {
                Id = entity.BitColId,
                NameEn = entity.BitColNameEn,
                NameAr = entity.BitColNameAr,
                HexCode = entity.BitColHexCode,
                ItemsColors = entity.BitItcItemsColors.Select(ic => new ItemColorResponseDto
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
                    }
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColorRequestDto request)
        {
            if (request == null) return BadRequest();

            var entity = new BitColColor
            {
                BitColNameEn = request.NameEn,
                BitColNameAr = request.NameAr,
                BitColHexCode = request.HexCode,
                BitColBitUsridEnterUser = currentUserID,
                BitColEnterDate = DateOnly.FromDateTime(DateTime.Now),
                BitColEnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var addedColor = await _colorService.AddAsync(entity);

            var response = new ColorResponseDto
            {
                Id = addedColor.BitColId,
                NameEn = addedColor.BitColNameEn,
                NameAr = addedColor.BitColNameAr,
                HexCode = addedColor.BitColHexCode
            };

            return CreatedAtAction(nameof(GetById), new { id = addedColor.BitColId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ColorRequestDto request)
        {
            var entity = await _colorService.GetBy(x => x.BitColId == id);
            if (entity == null) return NotFound();

            entity.BitColNameEn = request.NameEn;
            entity.BitColNameAr = request.NameAr;
            entity.BitColHexCode = request.HexCode;
            entity.BitColBitUsridModUser = currentUserID;
            entity.BitColModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitColModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _colorService.EditAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _colorService.GetBy(x => x.BitColId == id);
            if (entity == null) return NotFound();

            entity.BitColCancelled = true;
            entity.BitColBitUsridCancelledUser = currentUserID;
            entity.BitColCancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitColCancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _colorService.EditAsync(entity);
            return NoContent();
        }


        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsByColor(int id)
        {
            var itemsColors = await _IitemColorService.GetByListAsync(
                x => x.BitItcBitColid == id,
                includeProperties : x => x.BitItcBitItm
            );

            var itemsList = itemsColors.Select(ic => new ItemResponseDto
            {
                Id = ic.BitItcBitItm.BitItmId,
                NameEn = ic.BitItcBitItm.BitItmNameEn,
                NameAr = ic.BitItcBitItm.BitItmNameAr,
                DescriptionEn = ic.BitItcBitItm.BitItmDescriptionEn,
                DescriptionAr = ic.BitItcBitItm.BitItmDescriptionAr,
                Points = ic.BitItcBitItm.BitItmPoints,
                Status = ic.BitItcBitItm.BitItmStatus
            }).ToList();

            if (!itemsList.Any()) return NotFound();

            return Ok(itemsList);
        }

    }
}
