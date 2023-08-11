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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllAgreements()
        {
            try
            {
                var result = await _service.GetAllAgreementsAsync();
                
                return Ok(new ResponseDto<List<AgreementListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementListDto>>(result), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<AgreementListDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("search")]
        public async Task<IActionResult> SearchAllPayments(AgreementFilters filters)
        {
            try
            {
                var results = await _service.GetByFilters(filters);
                return Ok(new ResponseDto<List<AgreementListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementListDto>>(results)));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<AgreementListDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("company/{companyid}")]
        public async Task<IActionResult> GetAgreementsByCompanyId(int companyid)
        {
            try
            {
                return Ok(new ResponseDto<List<AgreementListDto>>(HttpStatusCode.OK, "", _mapper.Map<List<AgreementListDto>>(await _service.GetAgreementsByCompanyIdAsync(companyid)), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<AgreementListDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
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
        public async Task<IActionResult> Add(AgreementDto agreementDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = new Object();
                    if (agreementDto.Id > 0)
                    {
                        data = await _service.UpdateAgreementAsync(_mapper.Map<Agreement>(agreementDto));
                    }
                    else
                    {
                        data = await _service.AddAgreementAsync(_mapper.Map<Agreement>(agreementDto));
                    }
                    return Ok(new ResponseDto<AgreementDto>(HttpStatusCode.OK, "", agreementDto, null));
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
