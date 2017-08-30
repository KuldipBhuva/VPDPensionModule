using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class SAEmpDetailsViewModel
    {
        public long ID { get; set; }
        public Nullable<long> EmpID { get; set; }
        public Nullable<long> CompID { get; set; }
        public Nullable<long> SAID { get; set; }
        public Nullable<decimal> SAOpBal { get; set; }
        public string OpBalYear { get; set; }
        public int SAOBID { get; set; }
        public string EmpNo { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public string Year { get; set; }
        public Nullable<System.DateTime> SASettalDate { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }

        public List<SAEmpDetailsViewModel> lstsaemp { get; set; }
    }
}
