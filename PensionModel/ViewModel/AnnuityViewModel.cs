using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pension.Models.ViewModel;

namespace PensionModel.ViewModel
{
    public class AnnuityViewModel
    {
        public long Id { get; set; }
        public Nullable<long> LICId { get; set; }
        public Nullable<long> PlanID { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<decimal> Yly { get; set; }
        public Nullable<decimal> Hly { get; set; }
        public Nullable<decimal> Qly { get; set; }
        public Nullable<decimal> Mly { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Insurance { get; set; }
        public string PlanName { get; set; }
        public int hdnannuity { get; set; }

        public List<AnnuityViewModel> lstaty { get; set; }
        public List<AnnuityViewModel> listcheck { get; set; }
        public List<PlanViewModel> ListPlan { get; set; }
        public List<InsuranceViewModel> ListIns { get; set; }
    }
}
