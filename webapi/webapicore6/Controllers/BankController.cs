using AutoMapper;
using BAL.Interfaces;

using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using webapicore6.Models;
using webapicore6.Models.Common;

namespace webapicore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _service;
        private readonly IMapper _mapper;
        public BankController(IBankService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                
                return Ok(new ResponseDto<List<BankDto>>(HttpStatusCode.OK, "", _mapper.Map<List<BankDto>>(result), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<List<PaymentDto>>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankById(int id)
        {
            try
            {
                return Ok(new ResponseDto<BankDto>(HttpStatusCode.OK, "", _mapper.Map<BankDto>(await _service.GetByIdAsync(id)), null));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] BankDto dto)
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
                    
                    var data = await _service.AddAsync(_mapper.Map<Bank>(dto));
                    return Ok(new ResponseDto<BankDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] BankDto dto)
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

                    var model = _mapper.Map<Bank>(dto);
                    //model.RecUpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    var data = await _service.UpdateAsync(model);
                    return Ok(new ResponseDto<BankDto>(HttpStatusCode.OK, "", dto, null));
                }
                else
                {
                    var response = new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
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
                    var data = await _service.DeleteAsync(id);
                    return Ok(new ResponseDto<BankDto>(HttpStatusCode.OK, "", null, null));
                }
                else
                {
                    var response = new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, "Model is not valid.", null);
                    return Ok(response);
                }

            }
            catch (Exception e)
            {
                return Ok(new ResponseDto<BankDto>(HttpStatusCode.InternalServerError, e.Message, null, null, e.InnerException?.Message));
            }
        }
    }
}
