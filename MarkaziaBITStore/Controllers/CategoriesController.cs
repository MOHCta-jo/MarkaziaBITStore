using Azure.Core;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using MarkaziaWebCommon.Utils.CustomAttribute;
using MarkaziaWebCommon.Utils.Enums.AppRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Grpc.Core.Metadata;


namespace MarkaziaBITStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategory categoryService, ICurrentUserService currentUser, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoriesListParam param)
        {
            try
            {
                var pagingResult = await _categoryService.GetCategoriesList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetCategoriesListResult>
                    {
                        Data = new List<GetCategoriesListResult>(),
                        Message = "No categories found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetCategoriesListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving categories",
                    Error = ex
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService
                .GetBy(c => c.BitCatId == id, true, false, c => c.Include(x=> x.BitItmItems));

            if (category == null) return NotFound();


            var dto = new CategoryResponseDto
            {
                Id = category.BitCatId,
                NameEn = category.BitCatNameEn,
                NameAr = category.BitCatNameAr,
                IconUrl = category.BitCatIconUrl,
                IsActive = category.BitCatIsActive,
                Items = category.BitItmItems.Select(i => new ItemResponseDto
                {
                    Id = i.BitItmId,
                    NameEn = i.BitItmNameEn,
                    NameAr = i.BitItmNameAr
                }).ToList()
            };


            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  CategoryRequestDto request)
        {
            var entity = new BitCatCategory
            {
                BitCatNameEn = request.NameEn,
                BitCatNameAr = request.NameAr,
                BitCatIconUrl = request.IconUrl,
                BitCatIsActive = request.IsActive,

                BitCatBitUsridEnterUser = currentUserID,
                BitCatEnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BitCatEnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            };

            var categoryResult =  await _categoryService.AddAsync(entity);

            if (categoryResult == null) return NotFound();

            var response = new CategoryResponseDto
            {
                Id = entity.BitCatId,
                NameEn = entity.BitCatNameEn,
                NameAr = entity.BitCatNameAr,
                IconUrl = entity.BitCatIconUrl,
                IsActive = entity.BitCatIsActive
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.BitCatId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto request)
        {
            if (request is null || id <= 0) return BadRequest();

            var entity = await _categoryService.GetBy(x => x.BitCatId == id);

            entity.BitCatNameEn = request.NameEn;
            entity.BitCatNameAr = request.NameAr;
            entity.BitCatIconUrl = request.IconUrl;
            entity.BitCatIsActive = request.IsActive;

            // Update audit fields
            entity.BitCatBitUsridModUser = currentUserID;
            entity.BitCatModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitCatModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _categoryService.EditAsync(entity);


            return Ok(new CategoryResponseDto
            {
                NameEn = entity.BitCatNameEn,
                NameAr = entity.BitCatNameAr,
                IconUrl = entity.BitCatIconUrl,
                IsActive = entity.BitCatIsActive
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0 || id == null) return BadRequest();

                var entity = await _categoryService.GetBy(x => x.BitCatId == id);

                if (entity == null)
                    return NotFound();

                // Soft delete (Cancel)
                entity.BitCatCancelled = true;
                entity.BitCatBitUsridCancelledUser = currentUserID;
                entity.BitCatCancelledDate = DateOnly.FromDateTime(DateTime.Now);
                entity.BitCatCancelledTime = TimeOnly.FromDateTime(DateTime.Now);


                await _categoryService.EditAsync(entity);

                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet("{id}/items")]
        public async Task<IActionResult> GetItemsByCategory(int id)
        {
            var items = await _categoryService.
                GetByListAsync(c => c.BitCatId == id, true, false, c => c.BitItmItems);

            if (items == null || !items.Any()) return NotFound();

            return Ok(items);
        }
    }
}
