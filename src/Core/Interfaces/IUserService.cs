using Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserForAdminPanelViewModel>> GetUserListForAdimnPanelAsync();
        //Task<AddUserFromAdminPanelViewModel> AddUserFromAdminPanelAsync(AddUserFromAdminPanelViewModel user);
    }
}
