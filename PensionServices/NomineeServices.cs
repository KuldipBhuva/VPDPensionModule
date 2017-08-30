using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using System.Data.Entity.SqlServer;

namespace PensionServices
{
    public class NomineeServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        NomineeMaster nomiemst = new NomineeMaster();

        public List<NomineeViewModel> lstdata(int compid, int empid)
        {
            var lstdata = (from val in DbContext.NomineeMasters
                           where val.CompId == compid && val.EmpId == empid
                           select new NomineeViewModel()
                           {
                               id = val.id,
                               Name = val.Name,
                               Relation = val.Relation,
                               DOB = val.DOB,
                               Addess = val.Addess,
                               Contact = val.Contact,
                               Mobile = val.Mobile,
                               Adharnumber = val.Adharnumber,
                               Pan = val.Pan,
                               BankName = val.BankName,
                               Branch = val.Branch,
                               AccountNo = val.AccountNo,
                               IFSC = val.IFSC
                           }).ToList();
            return lstdata;
        }
        public NomineeViewModel GetByID(int id)
        {
            var lstdata = (from val in DbContext.NomineeMasters
                           where val.id == id
                           //let convertdate = val.DOB.HasValue ? val.DOB.Value.ToString("dd/MM/yyyy") : string.Empty
                           select new NomineeViewModel()
                           {
                               id = val.id,
                               Name = val.Name,
                               Relation = val.Relation,
                               DOB = val.DOB,//DateTime.Parse(convertdate),
                               //SqlFunctions.DateName("day", val.DOB).Trim() + "/" + SqlFunctions.StringConvert((double)val.DOB.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("year", val.DOB),
                               //convertdate,
                               //val.DOB.HasValue?val.DOB.Value.ToString("dd/MM/yyyy"):string.Empty,
                               Addess = val.Addess,
                               Contact = val.Contact,
                               Mobile = val.Mobile,
                               Adharnumber = val.Adharnumber,
                               Pan = val.Pan,
                               BankName = val.BankName,
                               Branch = val.Branch,
                               AccountNo = val.AccountNo,
                               IFSC = val.IFSC
                           }).FirstOrDefault();
            return lstdata;
        }
        public int insert(NomineeViewModel val)
        {
            nomiemst.CompId = val.CompId;
            nomiemst.EmpId = val.EmpId;
            nomiemst.Name = val.Name;
            nomiemst.Relation = val.Relation;
            nomiemst.DOB = val.DOB;
            nomiemst.Addess = val.Addess;
            nomiemst.Contact = val.Contact;
            nomiemst.Mobile = val.Mobile;
            nomiemst.Adharnumber = val.Adharnumber;
            nomiemst.Pan = val.Pan;
            nomiemst.BankName = val.BankName;
            nomiemst.Branch = val.Branch;
            nomiemst.AccountNo = val.AccountNo;
            nomiemst.IFSC = val.IFSC;
            DbContext.NomineeMasters.Add(nomiemst);
            DbContext.SaveChanges();
            return nomiemst.id;
        }
        public int update(NomineeViewModel val, int id)
        {
            nomiemst = DbContext.NomineeMasters.FirstOrDefault(m => m.id == id);

            //nomiemst.CompId = val.CompId;
            //nomiemst.EmpId = val.EmpId;
            nomiemst.Name = val.Name;
            nomiemst.Relation = val.Relation;
            nomiemst.DOB = val.DOB;
            nomiemst.Addess = val.Addess;
            nomiemst.Contact = val.Contact;
            nomiemst.Mobile = val.Mobile;
            nomiemst.Adharnumber = val.Adharnumber;
            nomiemst.Pan = val.Pan;
            nomiemst.BankName = val.BankName;
            nomiemst.Branch = val.Branch;
            nomiemst.AccountNo = val.AccountNo;
            nomiemst.IFSC = val.IFSC;
            return DbContext.SaveChanges();
        }
    }
}
