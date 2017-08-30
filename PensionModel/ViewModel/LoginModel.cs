using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace PensionModel.ViewModel
{
    public class LoginModel
    {
        public int UID { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> RID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "User Name Required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> Mobile { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public RoleModel RoleDetails { get; set; }
        public List<LoginModel> LoginList{get;set;}
    }
}
