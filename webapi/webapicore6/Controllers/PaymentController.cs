﻿using AutoMapper;
using BAL.Interfaces;
using DAL.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}