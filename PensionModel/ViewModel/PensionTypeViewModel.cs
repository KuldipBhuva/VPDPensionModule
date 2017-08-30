using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class PensionTypeViewModel
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }

        public List<PensionTypeViewModel> lstpen { get; set; }
        public int hdnpentype { get; set; }
    }
}
