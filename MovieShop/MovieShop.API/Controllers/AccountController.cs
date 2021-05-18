using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetProfile(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterRequestModel user)
        {
            var createdUser = await _userService.RegisterUser(user);
            return CreatedAtRoute("GetUser", new { id = createdUser.Id }, createdUser);
        }

        [HttpGet]
        public async Task<ActionResult> EmailExists([FromQuery] string email)
        {
            var user = await _userService.GetUser(email);
            return Ok(user == null ? new { emailExists = false } : new { emailExists = true });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginASync(UserLoginRequestModel userLoginRequestModel)
        {
            // check if the user has entered correct un/pw

            var user = await _userService.ValidateUser(userLoginRequestModel.Email, userLoginRequestModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var JwtToken = GenerateJWT(user);
            // if user entered valid un/pw
            // create JWT Token

            return Ok(new { token = JwtToken });

        }

        private string GenerateJWT(UserLoginResponseModel model)
        {
            // we will use the token libraries to create token

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, model.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, model.LastName)
            };

            // create identity object and store claims 
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // read the secret key from app settings, make sure secret key is unique and not guessable
            // In real world we use something like Azure KeyVault to store any secrets of application

            var secretKey = _configuration["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // get the expiration time of the token
            var expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:Expiration"));

            //pick an hashing algorithm 
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            // create the token object that yu will use to create the token that will include all the information such as credentials, claims, expiration time, 

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}
