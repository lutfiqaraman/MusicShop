using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicShop.API.ViewModels;
using MusicShop.Core.Models;
using MusicShop.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly IMapper Mapper;
        private readonly IConfiguration Configuration;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            UserService   = userService;
            Configuration = configuration;
            Mapper        = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authonticate([FromBody]UserVM userVM)
        {
            try
            {
                var user = await UserService.Authenticate(userVM.Username, userVM.Password);

                if (user == null)
                    return BadRequest();

                var tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));

                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(token);

                return Ok(new {
                    user.Id,
                    user.Username,
                    user.FirstName,
                    user.LastName,
                    tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserVM userVM)
        {
            User user = Mapper.Map<User>(userVM);

            try
            {
                User savedUser = await UserService.Create(user, userVM.Password);

                var tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));

                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.FirstName,
                    user.LastName,
                    tokenString
                });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IList<User> users = await UserService.GetAllUsers();
            var lstUser = Mapper.Map<IList<UserVM>>(users);

            return Ok(lstUser);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            User user = await UserService.GetUserById(Id);
            UserVM userVM = Mapper.Map<UserVM>(user);

            return Ok(userVM);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(int Id, [FromBody] UserVM userVM)
        {
            User user = Mapper.Map<User>(userVM);
            user.Id = Id;

            try
            {
                UserService.Update(user, userVM.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            UserService.Delete(Id);
            return Ok();
        }
    }
}
