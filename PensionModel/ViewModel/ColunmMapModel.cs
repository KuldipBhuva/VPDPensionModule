using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class ColunmMapModel
    {
        public bool OverWrite { get; set; }
        public int CMID { get; set; }
        public Nullable<int> CompID { get; set; }

        public string TblName { get; set; }
        public string TblClmName { get; set; }
        public Nullable<int> TblClmIndex { get; set; }
        public string ExlClmName { get; set; }
        public Nullable<int> ExlClmIndex { get; set; }
        public Nullable<int> MappedBy { get; set; }
        public Nullable<System.DateTime> MappedDate { get; set; }

        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public int ORDINAL_POSITION { get; set; }

        public SalaryModel SalaryModelDetails { get; set; }
        public PensionMasterModel PensionModelDetails { get; set; }
        public List<SalaryModel> ListSalary { get; set; }
        public List<ColunmMapModel> ListClm { get; set; }
        public TableColunm ClmDetails { get; set; }
    }
}
