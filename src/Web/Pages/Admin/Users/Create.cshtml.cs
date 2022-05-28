using Core.DTOs.User;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public AddUserFromAdminPanelViewModel User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    Name = User.Name,
                    UserName = User.UserName,
                    Email = User.Email,
                    EmailConfirmed = true
                };
                
                var result = await _userManager.CreateAsync(appUser, User.Password);

                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
