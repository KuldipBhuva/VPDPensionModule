using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class RetirementTypeViewModel
    {
        public int id { get; set; }
        public string RetirementType { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public int HdnRetirementID { get; set; }
        public List<RetirementTypeViewModel> lstRetirement { get; set; }

    }
}