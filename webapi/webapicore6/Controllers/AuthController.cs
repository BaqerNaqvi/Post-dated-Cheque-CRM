using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using webapicore6.Models.Common;
using webapicore6.Models.Identity;

namespace webapicore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, IConfiguration config, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userToCreate = new IdentityUser
                    {
                        Email = registerUserDto.Email,
                        UserName = registerUserDto.Username,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true
                    };
                    IdentityResult result = await _userManager.CreateAsync(userToCreate, registerUserDto.Password);
                    if (result.Succeeded)
                    {
                        //Assign Role
                        await _userManager.AddToRoleAsync(userToCreate, registerUserDto.RoleName);

                        var response = new ResponseDto<Object>(HttpStatusCode.Created, "User registered", registerUserDto);
                        return Ok(response);
                    }
                    else
                    {
                        var response = new ResponseDto<Object>(HttpStatusCode.Conflict, "Error", result.Errors);
                        return Conflict(response);
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRes = _mapper.Map<User>(user);
                    userRes.Role = roles.FirstOrDefault().ToString();
                    var response = new ResponseDto<Object>(HttpStatusCode.OK, "", new
                    {
                        token = GenerateJwtToken(user, roles),
                        user = userRes
                    });
                    return Ok(response);
                }
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}