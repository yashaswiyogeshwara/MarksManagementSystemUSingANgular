using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using MarksManagementSystem.DAL;
using MarksManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarksManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        public UserContext appContext { get; set; }
        public UserManager<IdentityUser> _userManager { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; set; }
        public DataContext _dataContext { get; set; }

        public AccountController(UserContext applicationContext, UserManager<IdentityUser> userManager,
         SignInManager<IdentityUser> signInManager, DataContext _dbContext)
        {
            this.appContext = applicationContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._dataContext = _dbContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserInfo user)
        {
            User userDetails = new User();
            try
            {
                using (DataContext context = _dataContext) {
                     userDetails = (from u in context.Users
                                        where u.Email == user.Email
                                        select u).FirstOrDefault<User>();
                    if (userDetails != null)
                    {
                        return Ok(new { Success = false,  message = "User already exists" });
                    }
                    else {
                        context.Users.Add(new User()
                        {
                            UserName = user.Name,
                            Password = user.Password,
                            Email = user.Email,
                            IsAdmin = user.IsAdmin
                        });
                        context.SaveChanges();
                        return Ok(new { Success = true, message = "User successfully created" });
                    }
                }
                                   
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, message = "Error while saving user,{0}",ex.Message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login data)
        {
            string json = "";
            Guid guid = new Guid();
            User userDetails = new User();
            try
            {
                using (DataContext context = _dataContext) {
                    userDetails = (from u in context.Users
                                   where u.Email.ToLower() == data.UserName.ToLower() && u.Password == data.Password
                                   select u).FirstOrDefault<User>();
                }
                if (userDetails != null)
                {
                    return Ok(new { success = true, token = guid });
                }
                else {
                    return Ok(new { success = false, message = "Please verify the username and password and enter again" });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error while logging into the system, {0}", ex.Message });
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
        public bool IsAdmin { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }
    }
}