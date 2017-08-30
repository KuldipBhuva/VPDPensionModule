using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class AllowanceViewModel
    {
        public int id { get; set; }
        public string AllowanceName { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public Nullable<int> Status { get; set; }

        public List<AllowanceViewModel>lstAllowace { get; set; }
        public int HdnAlloid { get; set; }




    }
}