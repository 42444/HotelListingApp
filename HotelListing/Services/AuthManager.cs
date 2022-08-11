using HotelListing.Data;
using HotelListing.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        public Task<string> CreateToken()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
