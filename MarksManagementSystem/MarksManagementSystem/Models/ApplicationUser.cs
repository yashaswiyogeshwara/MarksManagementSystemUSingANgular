using System;
using Microsoft.AspNetCore.Identity;

namespace MarksManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        private string _userName;

        public ApplicationUser()
        {
        }

        // private virtual string UserName { get => _userName; set => _userName = value; }
    }
}
