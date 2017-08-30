using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class SalaryModel
    {
        public bool OverWrite { get; set; }
        public int SID { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> SalaryMonth { get; set; }
        public string Year { get; set; }
        public Nullable<decimal> Basic { get; set; }
        public Nullable<decimal> HRA { get; set; }
        public Nullable<decimal> DA { get; set; }
        public Nullable<decimal> EA { get; set; }
        public Nullable<decimal> Conveyance { get; set; }
        public Nullable<decimal> Overtime { get; set; }
        public Nullable<decimal> PF { get; set; }
        public Nullable<decimal> Medical { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }

        public List<SalaryModel> ListSalary { get; set; }
    }
}
