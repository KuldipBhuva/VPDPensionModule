using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class PensionLimitViewModel
    {
        public long Id { get; set; }
        public Nullable<long> CmyID { get; set; }
        public string EmployeementType { get; set; }
        public Nullable<long> GradeID { get; set; }
        public Nullable<decimal> Pension_Limit { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
