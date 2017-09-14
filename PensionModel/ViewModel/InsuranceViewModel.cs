using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class InsuranceViewModel
    {
        public int id { get; set; }

        public int hdnInsuranceID { get; set; }
        [Required(ErrorMessage = "Please Enter Insurance Company")]
        [Display(Name = "Insurance Company")]
        public string InsuranceCompany { get; set; }
        public string HO { get; set; }
        public Nullable<int> Status { get; set; }
        public bool IsChecked { get; set; }
        public List<CompanyModel> lstcomp { get; set; }

        public List<InsuranceViewModel> lstins { get; set; }
    }
}