using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class NomineeViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Addess { get; set; }
        public string Contact { get; set; }
        public string Mobile { get; set; }
        public string Adharnumber { get; set; }
        public string Pan { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public Nullable<long> CompId { get; set; }
        public Nullable<long> EmpId { get; set; }

        public List<NomineeViewModel> lstnomei { get; set; }
        public int hdnnomieId { get; set; }
    }
}
