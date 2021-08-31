using api.Models;
using BuissnesLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        string _errorMessage;
        private AccountManager _accountManager;
        public AccountApiController(AccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                User _newUser = new User() { Phone = registerRequest.Phone, Email = registerRequest.Email, FIO = registerRequest.FIO, Password = registerRequest.Password };
                if (!_accountManager.Users.RegisteredUserForRegister(registerRequest.Phone, registerRequest.Email))
                {
                    _accountManager.Users.CreateUser(_newUser);
                    return Ok("");
                }
                _errorMessage = "Данный номер телефона или почта уже зарегистрирована!";
            }
            ErrorResponse _errorResponse = new ErrorResponse() { Code = BadRequest(ModelState).StatusCode, Message = _errorMessage };
            return BadRequest(_errorResponse);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                if (_accountManager.Users.RegisteredUser(loginRequest.Phone))
                {
                    if (_accountManager.Users.CorrectPassword(loginRequest.Phone, loginRequest.Password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType,loginRequest.Phone)
                        };
                        ClaimsIdentity phone = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(phone));
                        _accountManager.Users.LoginDate(loginRequest.Phone);
                       
                        return Ok(HttpContext.Request.Cookies);
                    }
                    else  _errorMessage =  "Введен некорректный пароль!";
                }
                else  _errorMessage = "Введен некорректный пароль!";
            }
            ErrorResponse _errorResponse = new ErrorResponse() { Code = BadRequest(ModelState).StatusCode, Message= _errorMessage };
            return BadRequest(_errorResponse);
        }
        [HttpGet]
        [Route("get-my-info")]
        public IActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                User userData = _accountManager.Users.GetUserByPhone(User.Identity.Name);
                return Ok(userData);
            }
            ErrorResponse _errorResponse = new ErrorResponse() { Code = BadRequest(ModelState).StatusCode, Message = "You are not authenticated!" };
            return BadRequest(_errorResponse );
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
