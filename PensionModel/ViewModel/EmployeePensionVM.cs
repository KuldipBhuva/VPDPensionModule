using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PensionModel.ViewModel;

namespace Pension.Models.ViewModel
{
   public class EmployeePensionVM
    {
        public Nullable<int> CompID { get; set; }
        public string PlanName { get; set; }
        public string Details { get; set; }
        public string Formula { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string PolicyNo { get; set; }
        public string AnnuityNo { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public Nullable<decimal> GrossAmt { get; set; }
        public Nullable<decimal> ITAmt { get; set; }
        public Nullable<decimal> CheqAmt { get; set; }
        public Nullable<bool> IsProcessed { get; set; }

        public List<EmployeePlanViewModel> lstempplan { get; set; }
        public List<EmployeePensionVM> lstemppension { get; set; }
    }
}
