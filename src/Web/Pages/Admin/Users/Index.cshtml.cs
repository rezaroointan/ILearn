using Core.DTOs.User;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserForAdminPanelViewModel> Users { get; set; }

        public async Task OnGet()
        {
            Users = await _userService.GetUserListForAdimnPanelAsync();
        }
    }
}
