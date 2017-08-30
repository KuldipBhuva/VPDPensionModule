using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pension.Models.ViewModel
{
    public class GradeViewModel
    {
        public int id { get; set; }
        public string Grade_Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public int hdnGradeID { get; set; }
        public List<GradeViewModel> lstgrade { get; set; }
    }
}