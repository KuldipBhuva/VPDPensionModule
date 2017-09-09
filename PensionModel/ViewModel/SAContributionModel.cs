using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
        public class SAContributionModel
        {
            public bool OverWrite { get; set; }
            public int SAID { get; set; }
            public string EmpNo { get; set; }
            public string EmpName { get; set; }
            public Nullable<decimal> Basic { get; set; }
            public Nullable<decimal> CompCont { get; set; }
            public Nullable<decimal> EmpCont { get; set; }
            public Nullable<int> CompID { get; set; }
            public int? Month { get; set; }
            public string Year { get; set; }
            public Nullable<System.DateTime> UploadedDate { get; set; }
            public Nullable<int> UploadedBy { get; set; }

            public List<SAContributionModel> ListSA { get; set; }
            public EmployeeViewModel EmpDetails { get; set; }
    }
}
