using Azure.Core;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using MarkaziaBITStore.Shared.CustomAttributes;
using MarkaziaBITStore.Shared.Enums.AppRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
                // Return only serializable error information
                return StatusCode(500, new
                {
                    Data = (string)null,
                    Message = "Error retrieving categories",
                    Error = new
                    {
                        Message = ex.Message,
                        ExceptionType = ex.GetType().Name,
                        StackTrace = ex.StackTrace
                    }
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService
                .GetBy(c => c.BIT_CAT_ID == id, true, false, c => c.Include(x=> x.BIT_ITM_Items));

            if (category == null) return NotFound();

            var dto = new CategoryResponseDto
            {
                Id = category.BIT_CAT_ID,
                NameEn = category.BIT_CAT_NameEN,
                NameAr = category.BIT_CAT_NameAR,
                IconUrl = category.BIT_CAT_IconURL,
                IsActive = category.BIT_CAT_IsActive,
                Items = category.BIT_ITM_Items.Select(i => new ItemResponseDto
                {
                    Id = i.BIT_ITM_ID,
                    NameEn = i.BIT_ITM_NameEN,
                    NameAr = i.BIT_ITM_NameAR
                }).ToList()
            };


            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  CategoryRequestDto request)
        {
            var entity = new BIT_CAT_Category
            {
                BIT_CAT_NameEN = request.NameEn,
                BIT_CAT_NameAR = request.NameAr,
                BIT_CAT_IconURL = request.IconUrl,
                BIT_CAT_IsActive = request.IsActive,

                BIT_CAT__MAS_USRID_EnterUser = currentUserID,
                BIT_CAT_EnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BIT_CAT_EnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            };

            var categoryResult =  await _categoryService.AddAsync(entity);

            if (categoryResult == null) return NotFound();

            var response = new CategoryResponseDto
            {
                Id = entity.BIT_CAT_ID,
                NameEn = entity.BIT_CAT_NameEN,
                NameAr = entity.BIT_CAT_NameAR,
                IconUrl = entity.BIT_CAT_IconURL,
                IsActive = entity.BIT_CAT_IsActive
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.BIT_CAT_ID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryRequestDto request)
        {
            if (request is null || id <= 0) return BadRequest();

            var entity = await _categoryService.GetBy(x => x.BIT_CAT_ID == id);

            entity.BIT_CAT_NameEN = request.NameEn;
            entity.BIT_CAT_NameAR = request.NameAr;
            entity.BIT_CAT_IconURL = request.IconUrl;
            entity.BIT_CAT_IsActive = request.IsActive;

            // Update audit fields
            entity.BIT_CAT__MAS_USRID_ModUser = currentUserID;
            entity.BIT_CAT_ModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_CAT_ModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _categoryService.EditAsync(entity);


            return Ok(new CategoryResponseDto
            {
                NameEn = entity.BIT_CAT_NameEN,
                NameAr = entity.BIT_CAT_NameAR,
                IconUrl = entity.BIT_CAT_IconURL,
                IsActive = entity.BIT_CAT_IsActive
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0 || id == null) return BadRequest();

                var entity = await _categoryService.GetBy(x => x.BIT_CAT_ID == id);

                if (entity == null)
                    return NotFound();

                // Soft delete (Cancel)
                entity.BIT_CAT_Cancelled = true;
                entity.BIT_CAT__MAS_USRID_CancelledUser = currentUserID;
                entity.BIT_CAT_CancelledDate = DateOnly.FromDateTime(DateTime.Now);
                entity.BIT_CAT_CancelledTime = TimeOnly.FromDateTime(DateTime.Now);


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
                GetByListAsync(c => c.BIT_CAT_ID == id, true, false, c => c.BIT_ITM_Items);

            if (items == null || !items.Any()) return NotFound();

            return Ok(items);
        }
    }
}
