using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockApp.Api.DTOs.Account;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;

namespace StockApp.Api.Controllers
{
    [Route("Api/Account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<StockUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<StockUser> userManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDto model)
        {
            StockUser user = new StockUser()
            {
                UserName = model.Username,
                Email=model.Email
            };

            var createUser = await _userManager.CreateAsync(user, model.Password);
            if (createUser.Succeeded)
                return Ok("Пользователь успешно зарегистрирован");
            else
                return BadRequest(createUser.Errors);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            var userByUsername = await _userManager.FindByNameAsync(model.Username);

            var userByEmail = await _userManager.FindByEmailAsync(model.Email);

            if ((userByEmail == null) && (userByUsername == null))
            {
                return BadRequest("Пользователя с таким логином или email не найдено");
            }
            else
            {
                var user = userByUsername ?? userByEmail;
                bool confirmed=await _userManager.CheckPasswordAsync(user, model.Password);

                if (!confirmed)
                    return BadRequest("Неправильный пароль");
                else
                {
                    var token = _tokenService.CreateToken(user);
                    return Ok(token);
                }
            }

        }

    }
}
