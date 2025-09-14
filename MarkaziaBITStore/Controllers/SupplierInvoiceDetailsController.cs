using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.RequestDTOs;
using MarkaziaBITStore.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkaziaBITStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierInvoiceDetailsController : ControllerBase
    {
        private readonly ISupplierInvoiceDetails _supplierInvoiceDetailsService;
        private readonly ICurrentUserService _currentUser;
        private readonly int currentUserID;

        public SupplierInvoiceDetailsController(
            ISupplierInvoiceDetails supplierInvoiceDetailsService,
            ICurrentUserService currentUser)
        {
            _supplierInvoiceDetailsService = supplierInvoiceDetailsService;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _supplierInvoiceDetailsService.GetAllAsQueryable()
                .ToListAsync();

            var response = list.Select(d => new SupplierInvoiceDetailResponseDto
            {
                Id = d.BitSidId,
                HeaderId = d.BitSidBitSihid,
                ItemColorId = d.BitSidBitItcid,
                Quantity = d.BitSidQuantity,
                UnitPrice = d.BitSidUnitPrice,
                Cancelled = d.BitSidCancelled
            });

            return Ok(response);
        }

        // GET by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BitSidId == id); ;
            if (entity == null) return NotFound();

            var response = new SupplierInvoiceDetailResponseDto
            {
                Id = entity.BitSidId,
                HeaderId = entity.BitSidBitSihid,
                ItemColorId = entity.BitSidBitItcid,
                Quantity = entity.BitSidQuantity,
                UnitPrice = entity.BitSidUnitPrice,
                Cancelled = entity.BitSidCancelled
            };

            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierInvoiceDetailRequestDto dto)
        {
            var entity = new BitSidSupplierInvoiceDetail
            {
                BitSidBitItcid = dto.ItemColorId,
                BitSidQuantity = dto.Quantity,
                BitSidUnitPrice = dto.UnitPrice,
                BitSidBitUsridEnterUser = currentUserID,
                BitSidEnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BitSidEnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            };

            await _supplierInvoiceDetailsService.AddAsync(entity);

            return Ok(new { entity.BitSidId });
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierInvoiceDetailRequestDto dto)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BitSidId == id);
            if (entity == null) return NotFound();

            entity.BitSidBitItcid = dto.ItemColorId;
            entity.BitSidQuantity = dto.Quantity;
            entity.BitSidUnitPrice = dto.UnitPrice;
            entity.BitSidBitUsridModUser = currentUserID;
            entity.BitSidModDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BitSidModTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceDetailsService.EditAsync(entity);

            return NoContent();
        }

        // DELETE (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BitSidId == id);
            if (entity == null) return NotFound();

            entity.BitSidCancelled = true;
            entity.BitSidBitUsridCancelledUser = currentUserID;
            entity.BitSidCancelledDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BitSidCancelledTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceDetailsService.EditAsync(entity);

            return NoContent();
        }
    }
}
