using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.User
{
    public class AddUserFromAdminPanelViewModel
    {

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا مقدار {0} را وارد نمایید.")]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
