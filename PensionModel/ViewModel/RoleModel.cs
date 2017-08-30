using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class RoleModel
    {
        public int RID { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
