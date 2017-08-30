using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class PaymentTypeModel
    {
        public int id { get; set; }
        public string PaymentType1 { get; set; }
        public string OtherCharge { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }

        public List<PaymentTypeModel> lstpay { get; set; }
        public int hdnpayid { get; set; }
    }
}
