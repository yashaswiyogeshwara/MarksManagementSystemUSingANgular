using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using MarksManagementSystem.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarksManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        public UserContext appContext { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; set; }

        public AccountController(UserContext applicationContext, UserManager<IdentityUser> userManager,
         SignInManager<IdentityUser> signInManager)
        {
            this.appContext = applicationContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<String> Register([FromBody] UserInfo user)
        {
            try
            {
                var result = await _userManager.CreateAsync(new IdentityUser { UserName = user.Name, Email = user.Email }, user.Password);

                if (result.Succeeded)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        Success = true
                    });
                }
                return JsonConvert.SerializeObject(new
                {
                    Success = false
                });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        [HttpPost("Login")]
        public async Task<string> Login(Login data)
        {
            string json = "";
            try
            {
                var signInStatus = await _signInManager.PasswordSignInAsync(data.UserName, data.Password, false, false);
                if (signInStatus.Succeeded)
                {

                    HttpContext.User = new GenericPrincipal(new ClaimsIdentity(data.UserName), new string[] { });



                    json = JsonConvert.SerializeObject(new
                    {
                        Success = true
                    });
                    return json;

                }
                return json = JsonConvert.SerializeObject(new
                {
                    Success = false
                });
            }
            catch (Exception ex)
            {
                return json = JsonConvert.SerializeObject(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }
    }

    public class Login
    {
        public string Password { get; set; }
        public string UserName { get; set; }

    }

    public class UserInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }
    }
}