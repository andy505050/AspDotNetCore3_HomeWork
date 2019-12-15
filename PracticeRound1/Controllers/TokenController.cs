using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticeRound1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("text/json")]

    public class TokenController : ControllerBase
    {
        [HttpPost("~/signin")]
        public ActionResult<string> SignIn(LoginViewModel login)
        {
            // 以下變數值應該透過 IConfiguration 取得
            var issuer = "JwtAuthDemo";
            var signKey = "1234567890123456"; // 請換成至少 16 字元以上的安全亂碼
            var expires = 30; // 單位: 分鐘

            if (ValidateUser(login))
            {
                return JwtHelpers.GenerateToken(issuer, signKey, login.Username, expires);
            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateUser(LoginViewModel login)
        {
            return true; // TODO
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("~/claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize(Roles = "Users,Admin")]
        [HttpGet("~/username")]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("~/jwtid")]
        public IActionResult GetUniqueId()
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}