using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class SAContributionViewModel
    {
        public long ID { get; set; }
        public Nullable<long> EmpID { get; set; }
        public Nullable<long> CompID { get; set; }
        public Nullable<long> SID { get; set; }
        public Nullable<decimal> Basic { get; set; }
        public Nullable<decimal> CompContri { get; set; }
        public Nullable<decimal> EmpContri { get; set; }
        public string Month { get; set; }
        public byte[] Year { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int SAID { get; set; }
        public string EmpNo { get; set; }
        public Nullable<decimal> CompCont { get; set; }
        public Nullable<decimal> EmpCont { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }

        public List<SAContributionViewModel> lstsacontri { get; set; }
    }
}
