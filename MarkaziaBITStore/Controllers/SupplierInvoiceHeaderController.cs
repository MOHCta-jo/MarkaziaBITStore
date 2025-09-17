using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
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
                x => x.BIT_SIH_ID == id,
                include: q => q.Include(h => h.BIT_SID_SupplierInvoiceDetails));

            if (entity == null) return NotFound();

            var response = new SupplierInvoiceHeaderResponseDto
            {
                Id = entity.BIT_SIH_ID,
                SupplierId = entity.BIT_SIH_SupplierID,
                SupplierInvNo = entity.BIT_SIH_SupplierInvNo,
                SupplierInvDate = entity.BIT_SIH_SupplierInvDate,
                SupplierInvoiceAmountNet = entity.BIT_SIH_SupplierInvoiceAmountNet,
                Cancelled = entity.BIT_SIH_Cancelled,
                Details = entity.BIT_SID_SupplierInvoiceDetails.Select(d => new SupplierInvoiceDetailResponseDto
                {
                    Id = d.BIT_SID_ID,
                    HeaderId = d.BIT_SID__BIT_SIHID,
                    ItemColorId = d.BIT_SID__BIT_ITCID,
                    Quantity = d.BIT_SID_Quantity,
                    UnitPrice = d.BIT_SID_UnitPrice,
                    Cancelled = d.BIT_SID_Cancelled
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierInvoiceHeaderRequestDto dto)
        {
            try
            {
                var entity = new BIT_SIH_SupplierInvoiceHeader
                {
                    BIT_SIH_SupplierInvNo = dto.SupplierInvNo,
                    BIT_SIH_SupplierInvDate = dto.SupplierInvDate,
                    BIT_SIH_SupplierInvoiceAmountNet = dto.SupplierInvoiceAmountNet,
                    BIT_SIH__MAS_USRID_EnterUser = currentUserID,
                    BIT_SIH_EnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    BIT_SIH_EnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
                };

                await _supplierInvoiceHeader.AddAsync(entity);

                return Ok(new { entity.BIT_SIH_ID });
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
                var entity = await _supplierInvoiceHeader.GetBy(x => x.BIT_SIH_SupplierID == id);
                if (entity == null) return NotFound();

                entity.BIT_SIH_SupplierInvNo = dto.SupplierInvNo;
                entity.BIT_SIH_SupplierInvDate = dto.SupplierInvDate;
                entity.BIT_SIH_SupplierInvoiceAmountNet = dto.SupplierInvoiceAmountNet;
                entity.BIT_SIH__MAS_USRID_ModUser = currentUserID;
                entity.BIT_SIH_ModDate = DateOnly.FromDateTime(DateTime.UtcNow);
                entity.BIT_SIH_ModTime = TimeOnly.FromDateTime(DateTime.UtcNow);

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
            var entity = await _supplierInvoiceHeader.GetBy(x=> x.BIT_SIH_SupplierID == id);
            if (entity == null) return NotFound();

            entity.BIT_SIH_Cancelled = true;
            entity.BIT_SIH__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_SIH_CancelledDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BIT_SIH_CancelledTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceHeader.EditAsync(entity);

            return NoContent();
        }

    }
}
