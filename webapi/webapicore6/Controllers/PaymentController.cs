using AutoMapper;
using BAL.Interfaces;
using DAL.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System.Net;
using System.Security.Claims;
using webapicore6.Models;
using webapicore6.Models.Common;

namespace webapicore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly IMapper _mapper;
        public PaymentController(IPaymentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var result = await _service.GetAllPaymentsAsync();
                
                return Ok(new ResponseDto<List<PaymentListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<PaymentListDto>>(result), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        [HttpPost("search")]
        public async Task<IActionResult> SearchAllPayments(PaymentFilters filters)
        {
            try
            {
                var results = await _service.GetByFilters(filters);
                return Ok(new ResponseDto<List<PaymentListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<PaymentListDto>>(results)));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<Payment>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }


        [AllowAnonymous]
        [HttpGet("{agreementid}")]
        public async Task<IActionResult> GetPaymentByAgreementId(int agreementid)
        {
            try
            {
                return Ok(new ResponseDto<List<PaymentListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<PaymentListDto>>(await _service.GetPaymentByAgreementIdAsync(agreementid)), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<PaymentDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<IActionResult> AddPayment([FromForm] PaymentDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Handle the file upload
                    //if (dto.MenuImage != null && dto.MenuImage.Length > 0)
                    //{
                    //    string fileName = Guid.NewGuid().ToString() + "_" + dto.MenuImage.FileName;
                    //    string filePath = Path.Combine("wwwroot/images/", fileName);

                    //    using (var stream = new FileStream(filePath, FileMode.Create))
                    //    {
                    //        await dto.MenuImage.CopyToAsync(stream);
                    //    }

                    //    // Update the DTO with the file path
                    //    dto.ImageUrl = fileName;
                    //}
                    //var model = _mapper.Map<Payment>(dto);
                    //model.RecCreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    //model.RecUpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    var data = await _service.AddPaymentAsync(_mapper.Map<Payment>(dto));
                    return Ok(new ResponseDto<PaymentDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<PaymentDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("update")]
        public async Task<IActionResult> UpdatePayment([FromForm] PaymentDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    // Handle the file upload
                    //if (dto.MenuImage != null && dto.MenuImage.Length > 0)
                    //{
                    //    // Delete the old MenuImage if it exists
                    //    if (!string.IsNullOrEmpty(dto.ImageUrl))
                    //    {
                    //        string oldImagePath = Path.Combine("wwwroot/images/", dto.ImageUrl);
                    //        if (System.IO.File.Exists(oldImagePath))
                    //        {
                    //            System.IO.File.Delete(oldImagePath);
                    //        }
                    //    }

                    //    string fileName = Guid.NewGuid().ToString() + "_" + dto.MenuImage.FileName;
                    //    string filePath = Path.Combine("wwwroot/images/", fileName);

                    //    using (var stream = new FileStream(filePath, FileMode.Create))
                    //    {
                    //        await dto.MenuImage.CopyToAsync(stream);
                    //    }

                    //    // Update the DTO with the file path
                    //    dto.ImageUrl = fileName;
                    //}

                    var model = _mapper.Map<Payment>(dto);
                    //model.RecUpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    var data = await _service.UpdatePaymentAsync(model);
                    return Ok(new ResponseDto<PaymentDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<PaymentDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _service.DeletePaymentAsync(id);
                    return Ok(new ResponseDto<PaymentDto>(HttpStatusCode.OK, "", null, null));
                }
                else
                {
                    var response = new ResponseDto<PaymentDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".xls")
                {
                    return BadRequest("Invalid file format. Only XLS format is supported.");
                }

                var rowDataList = new List<Dictionary<string, object>>();

                using (var stream = file.OpenReadStream())
                {
                    var workbook = new HSSFWorkbook(stream);
                    var worksheet = workbook.GetSheetAt(0);

                    if (worksheet == null)
                    {
                        return BadRequest("Invalid Excel file format.");
                    }

                    int endingRow = 40;
                    for (int row = worksheet.LastRowNum; row >= endingRow; row--)
                    {
                        var currentRow = worksheet.GetRow(row);
                        var cell18 = currentRow.GetCell(18);//Description

                        if (cell18 != null)
                        {
                            var cell18Value = cell18.StringCellValue;
                            if (cell18Value == "Closing Balance")
                            {
                                endingRow = row + 1;
                            }
                            else if (cell18Value != "Opening Balance" && (cell18Value.Contains("PDC Deposit") || cell18Value.Contains("Returned Cheque")))
                            {
                                var rowData = new Dictionary<string, object>();

                                var headers = new[] { 7, 9, 18, 29, 30 };
                                for (int headerIndex = 0; headerIndex < headers.Length; headerIndex++)
                                {
                                    var cell = currentRow.GetCell(headers[headerIndex]);
                                    var header = GetHeaderName(headers[headerIndex]);
                                    rowData.Add(header, GetCellValue(cell));
                                }

                                rowDataList.Add(rowData);
                            }
                        }
                    }

                    var results = await _service.ProcessImportedData(rowDataList);

                    // Filter distinct rows based on PaymentListDto.Id
                    var distinctResults = results.GroupBy(p => p.Id).Select(g => g.First()).ToList();

                    return Ok(new ResponseDto<List<PaymentListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<PaymentListDto>>(distinctResults)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error occurred during import: {ex.Message}");
            }
        }

        private string GetHeaderName(int cellAddress)
        {
            switch (cellAddress)
            {
                case 7: return "TRAN DATE";
                case 9: return "REF NO / CHQ NO";
                case 18: return "DESCRIPTION";
                case 29: return "DEBIT";
                case 30: return "CREDIT";
                default: return string.Empty;
            }
        }

        private object GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return null;
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                // Handle other cell types as needed
                default:
                    return null;
            }
        }

    }
}
