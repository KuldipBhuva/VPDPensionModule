using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class CompWiseAllowViewModel
    {
        public int id { get; set; }
        public bool IsChecked { get; set; }
        public int hdnCompAlloid { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> AllowanceID { get; set; }
        public Nullable<System.DateTime> EffDeate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> GradeID { get; set; }

        public string CompName { get; set; }
        public string Grade { get; set; }
        public string AllowName { get; set; }
        public string Desc { get; set; }
        public List<CompanyModel> lstComp { get; set; }
        public List<AllowanceViewModel> lstAllo { get; set; }
        public List<CompWiseAllowViewModel> lstCompAllo { get; set; }
        public List<GradeViewModel> lstgrade { get; set; }
    }
}