using Core.DTOs.User;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    public class DetailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public DetailModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public DetailUserForAdminPanelViewModel UserDetail { get; set; }

        public async Task<IActionResult> OnGet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
            {
                return NotFound();
            }

            UserDetail = new DetailUserForAdminPanelViewModel
            {
                Name = appUser.Name,
                UserName = appUser.UserName,
                Email = appUser.Email
            };

            return Page();
        }
    }
}
