using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pension.Models.ViewModel;

namespace PensionModel.ViewModel
{
    public class CompanyModel
    {
        public int id { get; set; }
        public string CompCode { get; set; }
        public string CompName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Type { get; set; }
        public Nullable<int> WidowPension { get; set; }
        public Nullable<int> PensionEligibityYears { get; set; }
        public Nullable<int> Status { get; set; }
        public string PensionLimit { get; set; }
        public string Merging { get; set; }
        public Nullable<int> SAIntMethod { get; set; }
        public Nullable<decimal> SAContriRate { get; set; }
        public Nullable<int> SAStopIntType { get; set; }
        public Nullable<int> SAContriMethod { get; set; }
        public Nullable<int> EligibityYr { get; set; }

        //common
        public Nullable<long> CmyID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }


        // SA Interest
        public Nullable<long> SaID { get; set; }
        public Nullable<decimal> LiveEmpInt { get; set; }
        public Nullable<decimal> LeftEmpInt { get; set; }
        public string LiveEffYear { get; set; }
        public string LeftEffYear { get; set; }
        
        // Merging Company
        public long MerId { get; set; }
        public Nullable<long> MCmyID { get; set; }
        public Nullable<System.DateTime> MergeEffDate { get; set; }
        public Nullable<int> MergeStatus { get; set; }

        public List<CompanyModel> ListMergeComp { get; set; }

        // Pension Limit
        public long PenId { get; set; }
        public string EmployeementType { get; set; }
        public Nullable<long> GradeID { get; set; }
        public string GradeName { get; set; }
        public Nullable<int> PenStatus { get; set; }
        public Nullable<System.DateTime> PenEffDate { get; set; }
        public Nullable<int> PensionType { get; set; }
        public Nullable<decimal> Pension_Limit { get; set; }
        public int hdnPenId { get; set; }

        public List<CompanyModel> ListPension { get; set; }
        public List<GradeViewModel> Listgrade { get; set; }

        public List<CompanyModel> LstCmy { get; set; }
        public List<CompanyModel> ListComp { get; set; }
        public List<CompanyModel> ListSA { get; set; }
    }
}
