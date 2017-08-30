using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class PlanViewModel
    {
        public int id { get; set; }
        public int HdnPlanID { get; set; }
        public Nullable<int> CompID { get; set; }
        public string PlanName { get; set; }
        public string Details { get; set; }
        public string Formula { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<int> Status { get; set; }
        public List<PlanViewModel> lstPlan { get; set; }
        public List<InsuranceViewModel> lstInsurance { get; set; }
    }
}