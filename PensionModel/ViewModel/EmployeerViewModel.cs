using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class EmployeerViewModel
    {
        public int id { get; set; }
        public Nullable<int> CompID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
        public int hdnEmployeerId { get; set; }
        public List<EmployeerViewModel> lstemployeer { get; set; }
        public List<CompanyModel> lstcomp { get; set; }

        public CompanyModel CompDetails { get; set; }
    }
}