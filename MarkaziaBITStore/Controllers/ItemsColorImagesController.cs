using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.Entites;
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
                .Include(img => img.BitIciBitItc)
                .ToListAsync();

            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _itemColorImageService.GetBy(
                x => x.BitIciId == id,
                include: q => q.Include(i => i.BitIciBitItc)
            );

            if (entity == null) return NotFound();

            var response = new ItemColorImageResponseDto
            {
                Id = entity.BitIciId,
                Sequence = entity.BitIciSequence,
                ImageUrl = entity.BitIciImageUrl,
                IsDefault = entity.BitIciIsDefault,
                Status = entity.BitIciStatus
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemColorImageRequestDto request, int itemColorId)
        {
            if (request == null) return BadRequest();

            var entity = new BitIciItemsColorImage
            {
                BitIciBitItcid = itemColorId,  
                BitIciSequence = request.Sequence,
                BitIciImageUrl = request.ImageUrl,
                BitIciIsDefault = request.IsDefault,
                BitIciStatus = request.Status,
                BitIciBitUsridEnterUser = currentUserID,
                BitIciEnterDate = DateOnly.FromDateTime(DateTime.Now),
                BitIciEnterTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            var added = await _itemColorImageService.AddAsync(entity);

            var response = new ItemColorImageResponseDto
            {
                Id = added.BitIciId,
                Sequence = added.BitIciSequence,
                ImageUrl = added.BitIciImageUrl,
                IsDefault = added.BitIciIsDefault,
                Status = added.BitIciStatus
            };

            return CreatedAtAction(nameof(GetById), new { id = added.BitIciId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemColorImageRequestDto request)
        {
            var entity = await _itemColorImageService.GetBy(x => x.BitIciId == id);
            if (entity == null) return NotFound();

            entity.BitIciSequence = request.Sequence;
            entity.BitIciImageUrl = request.ImageUrl;
            entity.BitIciIsDefault = request.IsDefault;
            entity.BitIciStatus = request.Status;
            entity.BitIciBitUsridModUser = currentUserID;
            entity.BitIciModDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitIciModTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorImageService.EditAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _itemColorImageService.GetBy(x => x.BitIciId == id);
            if (entity == null) return NotFound();

            entity.BitIciCancelled = true;
            entity.BitIciBitUsridCancelledUser = currentUserID;
            entity.BitIciCancelledDate = DateOnly.FromDateTime(DateTime.Now);
            entity.BitIciCancelledTime = TimeOnly.FromDateTime(DateTime.Now);

            await _itemColorImageService.EditAsync(entity);

            return NoContent();
        }


    }
}
