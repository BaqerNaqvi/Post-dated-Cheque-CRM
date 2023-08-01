using AutoMapper;
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
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService _service;
        private readonly IMapper _mapper;
        public AgreementController(IAgreementService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAgreements()
        {
            try
            {
                var result = await _service.GetAllAgreementsAsync();
                
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementDto>>(result), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        [HttpPost("search")]
        public async Task<IActionResult> SearchAllPayments(AgreementFilters filters)
        {
            try
            {
                var results = await _service.GetByFilters(filters);
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementDto>>(results)));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<Payment>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }


        [AllowAnonymous]
        [HttpGet("company/{companyid}")]
        public async Task<IActionResult> GetAgreementsByCompanyId(int companyid)
        {
            try
            {
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementDto>>(await _service.GetAgreementsByCompanyIdAsync(companyid)), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<PaymentDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgreementById(int id)
        {
            try
            {
                return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.OK, "", _mapper.Map<AgreementDto>(await _service.GetAgreementAsync(id)), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AgreementDto dto)
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
                    
                    var data = await _service.AddAgreementAsync(_mapper.Map<Agreement>(dto));
                    return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<AgreementDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] AgreementDto dto)
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

                    var model = _mapper.Map<Agreement>(dto);
                    //model.RecUpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    var data = await _service.UpdateAgreementAsync(model);
                    return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<AgreementDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _service.DeleteAgreementAsync(id);
                    return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.OK, "", null, null));
                }
                else
                {
                    var response = new ResponseDto<AgreementDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<AgreementDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }
    }
}
