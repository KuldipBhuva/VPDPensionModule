using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class SAEmpDetailsModel
    {
        public bool OverWrite { get; set; }
        public int SAOBID { get; set; }
        public string EmpNo { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public string Year { get; set; }
        public Nullable<int> SAID { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<System.DateTime> SASettalDate { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }

        public EmployeeViewModel EmpDetails { get; set; }
        public List<SAEmpDetailsModel> ListSA { get; set; }
    }
}
