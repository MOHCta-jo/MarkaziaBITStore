using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.Contracts.User;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
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
        public async Task<IActionResult> GetAll([FromQuery] GetSupplierInvoiceDetailsListParam param)
        {
            try
            {
                var pagingResult = await _supplierInvoiceDetailsService.GetSupplierInvoiceDetailsList(param);

                if (pagingResult.Data == null || pagingResult.Data.Count == 0)
                {
                    return Ok(new PagingResultWrapper<GetSupplierInvoiceDetailsListResult>
                    {
                        Data = new List<GetSupplierInvoiceDetailsListResult>(),
                        Message = "No supplier invoice details found",
                        Error = null,
                        PageNo = param.PageNo,
                        PageSize = param.PageSize,
                        TotalCount = 0,
                        PageCount = 0
                    });
                }

                return Ok((PagingResultWrapper<GetSupplierInvoiceDetailsListResult>)pagingResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultWrapper<string>
                {
                    Data = null!,
                    Message = "Error retrieving supplier invoice details",
                    Error = ex
                });
            }
        }
        // GET by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BIT_SID_ID == id); ;
            if (entity == null) return NotFound();

            var response = new SupplierInvoiceDetailResponseDto
            {
                Id = entity.BIT_SID_ID,
                HeaderId = entity.BIT_SID__BIT_SIHID,
                ItemColorId = entity.BIT_SID__BIT_ITCID,
                Quantity = entity.BIT_SID_Quantity,
                UnitPrice = entity.BIT_SID_UnitPrice,
                Cancelled = entity.BIT_SID_Cancelled
            };

            return Ok(response);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierInvoiceDetailRequestDto dto)
        {
            var entity = new BIT_SID_SupplierInvoiceDetails
            {
                BIT_SID__BIT_ITCID = dto.ItemColorId,
                BIT_SID_Quantity = dto.Quantity,
                BIT_SID_UnitPrice = dto.UnitPrice,
                BIT_SID__MAS_USRID_EnterUser = currentUserID,
                BIT_SID_EnterDate = DateOnly.FromDateTime(DateTime.UtcNow),
                BIT_SID_EnterTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            };

            await _supplierInvoiceDetailsService.AddAsync(entity);

            return Ok(new { entity.BIT_SID_ID });
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierInvoiceDetailRequestDto dto)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BIT_SID_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_SID__BIT_ITCID = dto.ItemColorId;
            entity.BIT_SID_Quantity = dto.Quantity;
            entity.BIT_SID_UnitPrice = dto.UnitPrice;
            entity.BIT_SID__MAS_USRID_ModUser = currentUserID;
            entity.BIT_SID_ModDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BIT_SID_ModTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceDetailsService.EditAsync(entity);

            return NoContent();
        }

        // DELETE (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _supplierInvoiceDetailsService.GetBy(x => x.BIT_SID_ID == id);
            if (entity == null) return NotFound();

            entity.BIT_SID_Cancelled = true;
            entity.BIT_SID__MAS_USRID_CancelledUser = currentUserID;
            entity.BIT_SID_CancelledDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.BIT_SID_CancelledTime = TimeOnly.FromDateTime(DateTime.UtcNow);

            await _supplierInvoiceDetailsService.EditAsync(entity);

            return NoContent();
        }
    }
}
