using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pension.Models.ViewModel;

namespace PensionModel.ViewModel
{
    public class GradeChangeViewModel
    {
        public int id { get; set; }
        public Nullable<int> Compnay_id { get; set; }
        public Nullable<int> EmpNo { get; set; }
        public Nullable<int> GradeChangeTo { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public string GradeName { get; set; }

        public List<GradeChangeViewModel> lstgrade { get; set; }
        public List<GradeViewModel> listgrade { get; set; }
        public int hdngradeId { get; set; }
    }
}
