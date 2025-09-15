using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Services;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarkaziaBITStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierInvoiceHeaderController : ControllerBase
    {
        private readonly ISupplierInvoiceHeader _supplierInvoiceHeader;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<SupplierInvoiceHeaderController> _logger;

        private readonly int currentUserID;

        public SupplierInvoiceHeaderController(
            ISupplierInvoiceHeader supplierInvoiceHeader,
            ICurrentUserService currentUser,
            ILogger<SupplierInvoiceHeaderController> logger)
        {
            _supplierInvoiceHeader = supplierInvoiceHeader;
            _currentUser = currentUser;
            currentUserID = _currentUser.GetUserId();
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetSupplierInvoiceHeadersListParam param)
        {
            try
            {
                var pagingResult = await _supplierInvoiceHeader.GetSupplierInvoiceHeadersList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetSupplierInvoiceHeadersListResult>
                    {
                        Data = new List<GetSupplierInvoiceHeadersListResult>(),
                        Message = "No supplier invoice headers found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetSupplierInvoiceHeadersListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving supplier invoice headers",
                    Error = ex
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _supplierInvoiceHeader.GetBy(
                x => x.BitSihId == id,
                include: q => q.Include(h => h.BitSidSupplierInvoiceDetails));

            if (entity == null) return NotFound();

            var response = new SupplierInvoiceHeaderResponseDto
            {
                Id = entity.BitSihId,
                SupplierId = entity.BitSihSupplierId,
                SupplierInvNo = entity.BitSihSupplierInvNo,
                SupplierInvDate = entity.BitSihSupplierInvDate,
                SupplierInvoiceAmountNet = entity.BitSihSupplierInvoiceAmountNet,
                Cancelled = entity.BitSihCancelled,
                Details = entity.BitSidSupplierInvoiceDetails.Select(d => new SupplierInvoiceDetailResponseDto
                {
                    Id = d.BitSidId,
                    HeaderId = d.BitSidBitSihid,
                    ItemColorId = d.BitSidBitItcid,
                    Quantity = d.BitSidQuantity,
                    UnitPrice = d.BitSidUnitPrice,
                    Cancelled = d.BitSidCancelled
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierInvoiceHeaderRequestDto dto)
        {
            try
            {
                var entity = new BitSihSupplierInvoiceHeader
                {
                    BitSihSupplierInvNo = dto.SupplierInvNo,
                    BitSihSupplierInvDate = dto.SupplierInvDate,
                    BitSihSupplierInvoiceAmountNet = dto.SupplierInvoiceAmountNet,
                    BitSihBitUsridEnterUser = currentUserID,
                    BitSihEnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    BitSihEnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
                };

                await _supplierInvoiceHeader.AddAsync(entity);

                return Ok(new { entity.BitSihId });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierInvoiceHeaderRequestDto dto)
        {
            try
            {
                var entity = await _supplierInvoiceHeader.GetBy(x => x.BitSihSupplierId == id);
                if (entity == null) return NotFound();

                entity.BitSihSupplierInvNo = dto.SupplierInvNo;
                entity.BitSihSupplierInvDate = dto.SupplierInvDate;
                entity.BitSihSupplierInvoiceAmountNet = dto.SupplierInvoiceAmountNet;
                entity.BitSihBitUsridModUser = currentUserID;
                entity.BitSihModDate = DateOnly.FromDateTime(DateTime.UtcNow);
                entity.BitSihModTime = TimeOnly.FromDateTime(DateTime.UtcNow);

                await _supplierInvoiceHeader.EditAsync(entity);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _supplierInvoiceHeader.GetBy(x=> x.BitSihSupplierId == id);
            if (entity == null) return NotFound();

            entity.BitSihCancelled = true;
            entity.BitSihBitUsridCancelledUser = currentUserID;
            entity.BitSihCancelledDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BitSihCancelledTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceHeader.EditAsync(entity);

            return NoContent();
        }

    }
}
