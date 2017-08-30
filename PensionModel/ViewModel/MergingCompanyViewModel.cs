using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel
{
    public class MergingCompanyViewModel
    {
        public long Id { get; set; }
        public Nullable<long> CmyID { get; set; }
        public Nullable<long> MCmyID { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
