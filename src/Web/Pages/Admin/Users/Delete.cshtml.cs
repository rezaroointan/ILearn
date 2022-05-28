using Core.DTOs.User;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    public class DeleteModel : PageModel
    {
        public readonly UserManager<AppUser> _userManager;

        public DeleteModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public DetailUserForAdminPanelViewModel DetailUser { get; set; }

        public async Task<IActionResult> OnGet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            DetailUser = new DetailUserForAdminPanelViewModel
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email
            };

            return Page();
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
