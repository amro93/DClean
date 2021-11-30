using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DClean.Application.DTOs.Account;
using DClean.Application.Interfaces;
using DClean.Application.Wrappers;
using DClean.Infrastructure.Persistence.Contexts;

namespace DClean.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return await _accountService.AuthenticateAsync(request, GenerateIPAddress());
        }
        [HttpPost("register")]
        public async Task<Response<string>> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return await _accountService.RegisterAsync(request, origin);
        }
        [HttpGet("confirm-email")]
        public async Task<Response<string>> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return await _accountService.ConfirmEmailAsync(userId, code);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {

            return await _accountService.ResetPassword(model);
        }
        //[HttpPost("migrate")]
        //public IActionResult Migrate([FromServices]DCleanDbContext mainDbContext)
        //{
        //    mainDbContext.Database.EnsureCreated();
        //    return Ok();
        //}
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}