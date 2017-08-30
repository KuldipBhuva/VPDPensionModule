using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class SAInterestViewModel
    {
        public long ID { get; set; }
        public Nullable<long> CompID { get; set; }
        public Nullable<decimal> LiveEmpInt { get; set; }
        public Nullable<decimal> LeftEmpInt { get; set; }
        public string LiveEffYear { get; set; }
        public string LeftEffYear { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
