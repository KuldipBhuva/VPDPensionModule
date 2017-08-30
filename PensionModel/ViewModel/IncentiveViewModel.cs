using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class IncentiveViewModel
    {
        public long Id { get; set; }
        public Nullable<int> From { get; set; }
        public Nullable<int> To { get; set; }
        public Nullable<decimal> Yly { get; set; }
        public Nullable<decimal> Hly { get; set; }
        public Nullable<decimal> Qly { get; set; }
        public Nullable<decimal> Mly { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<int> Status { get; set; }

        public int hdnin { get; set; }
        public List<IncentiveViewModel> lstin { get; set; }
    }
}
