using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace MarkaziaBITStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsColorImagesController : Controller
    {
        private readonly IitemsColorImage _itemColorImageService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;
        public ItemsColorImagesController(IitemsColorImage itemColorImageService, 
            ICurrentUserService currentUser)
        {
            _itemColorImageService = itemColorImageService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // should use paging here
            var list = await _itemColorImageService.GetAllAsQueryable()
                .Include(img => img.BIT_ICI__BIT_ITC)
                .ToListAsync();

            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _itemColorImageService.GetBy(
                x => x.BIT_ICI_ID == id,
                include: q => q.Include(i => i.BIT_ICI__BIT_ITC)
            );

            if (entity == null) return NotFound();

            var response = new ItemColorImageResponseDto
            {
                Id = entity.BIT_ICI_ID,
                Sequence = entity.BIT_ICI_ScreenSequence,
                ImageUrl = entity.BIT_ICI_ImageURL,
                IsDefault = entity.BIT_ICI_IsDefault,
                Status = entity.BIT_ICI_Status
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemColorImageRequestDto request, int itemColorId)
        {
            if (request == null) return BadRequest();

            var entity = new BIT_ICI_ItemsColorImages
            {
                BIT_ICI__BIT_ITCID = itemColorId,  
                BIT_ICI_ScreenSequence = request.Sequence,
                BIT_ICI_ImageURL = request.ImageUrl,
                BIT_ICI_IsDefault = request.IsDefault,
                BIT_ICI_Status = request.Status,
                BIT_ICI__MAS_USRID_EnterUser = currentUserID,
                BIT_ICI_EnterDate = DateOnly.FromDateTime(DateTime.Now),
                BIT_ICI_EnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var added = await _itemColorImageService.AddAsync(entity);

            var response = new ItemColorImageResponseDto
            {
                Id = added.BIT_ICI_ID,
                Sequence = added.BIT_ICI_ScreenSequence,
                ImageUrl = added.BIT_ICI_ImageURL,
                IsDefault = added.BIT_ICI_IsDefault,
                Status = added.BIT_ICI_Status
            };

            return CreatedAtAction(nameof(GetById), new { id = added.BIT_ICI_ID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemColorImageRequestDto request)
        {
            var entity = await _itemColorImageService.GetBy(x => x.BIT_ICI_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ICI_ScreenSequence = request.Sequence;
            entity.BIT_ICI_ImageURL = request.ImageUrl;
            entity.BIT_ICI_IsDefault = request.IsDefault;
            entity.BIT_ICI_Status = request.Status;
            entity.BIT_ICI__MAS_USRID_ModUser = currentUserID;
            entity.BIT_ICI_ModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ICI_ModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorImageService.EditAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _itemColorImageService.GetBy(x => x.BIT_ICI_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_ICI_Cancelled = true;
            entity.BIT_ICI__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_ICI_CancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BIT_ICI_CancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorImageService.EditAsync(entity);

            return NoContent();
        }


    }
}
