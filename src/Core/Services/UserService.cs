using Core.DTOs.User;
using Core.Interfaces;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly ILearnDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public UserService(ILearnDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<UserForAdminPanelViewModel>> GetUserListForAdimnPanelAsync()
        {
            return await _dbContext.Users.Select(u => new UserForAdminPanelViewModel { Id = u.Id, Name = u.Name, UserName = u.UserName, Email = u.Email, EmailConfirmed = u.EmailConfirmed }).ToListAsync();
        }
    }
}
