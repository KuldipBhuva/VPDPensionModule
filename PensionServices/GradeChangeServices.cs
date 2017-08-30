using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using Pension.Models.ViewModel;

namespace PensionServices
{
    public class GradeChangeServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        GradeChange grademst = new GradeChange();
        GradeChangeViewModel lstmodel = new GradeChangeViewModel();

        public List<GradeChangeViewModel> lstgrade(int empno, int compid)
        {
            try
            {
                var lstgrade = (from val in DbContext.GradeChanges
                                join val1 in DbContext.GradeMasters on val.GradeChangeTo equals val1.id
                                //join val2 in DbContext.EmployeeMasters on val.Compnay_id equals val2.CompId
                                where val.EmpNo == empno && val.Compnay_id == compid
                                select new GradeChangeViewModel()
                                {
                                    id = val.id,
                                    GradeName = val1.Grade_Name,
                                    GradeChangeTo = val.GradeChangeTo,
                                    EffDate = val.EffDate
                                }).ToList();
                return lstgrade;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "lstgrade", ex);
                return null;
            }
        }

        public GradeChangeViewModel CheckExists(int Grade, int empno)
        {
            try
            {
                var CompData = (from comp in DbContext.GradeChanges
                                where comp.GradeChangeTo == Grade && comp.EmpNo == empno
                                select new GradeChangeViewModel()
                                {
                                    GradeChangeTo = comp.GradeChangeTo
                                }).SingleOrDefault();

                return CompData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "CheckExists", ex);
                return null;
            }
        }

        public int insert(GradeChangeViewModel model)
        {
            grademst.Compnay_id = model.Compnay_id;
            grademst.EmpNo = model.EmpNo;
            grademst.GradeChangeTo = model.GradeChangeTo;
            grademst.EffDate = model.EffDate;
            DbContext.GradeChanges.Add(grademst);
            DbContext.SaveChanges();

            EmployeeMaster empmst = new EmployeeMaster();
            var i = DbContext.GradeChanges.Where(m => m.EmpNo == model.EmpNo && m.Compnay_id == model.Compnay_id).OrderByDescending(m => m.EffDate).FirstOrDefault();
            empmst = DbContext.EmployeeMasters.FirstOrDefault(m => m.EmpId == i.EmpNo);
            empmst.GradeId = i.GradeChangeTo;
            DbContext.SaveChanges();
            return grademst.id;
        }

        public int update(GradeChangeViewModel model, int id)
        {
            grademst = DbContext.GradeChanges.FirstOrDefault(m => m.id == id);
            grademst.Compnay_id = model.Compnay_id;
            grademst.EmpNo = model.EmpNo;
            grademst.GradeChangeTo = model.GradeChangeTo;
            grademst.EffDate = model.EffDate;
            DbContext.SaveChanges();

            EmployeeMaster empmst = new EmployeeMaster();
            var i = DbContext.GradeChanges.Where(m => m.EmpNo == model.EmpNo && m.Compnay_id == model.Compnay_id).OrderByDescending(m => m.EffDate).FirstOrDefault();
            empmst = DbContext.EmployeeMasters.FirstOrDefault(m => m.EmpId == i.EmpNo);
            empmst.GradeId = i.GradeChangeTo;

            return DbContext.SaveChanges();
        }

        public int delete(int id)
        {
            grademst = DbContext.GradeChanges.Find(id);
            DbContext.GradeChanges.Remove(grademst);
            return DbContext.SaveChanges();
        }

        public GradeChangeViewModel GetByID(int id)
        {
            var lstdata = (from val in DbContext.GradeChanges
                           where val.id == id
                           select new GradeChangeViewModel()
                           {
                               Compnay_id = val.Compnay_id,
                               EmpNo = val.EmpNo,
                               GradeChangeTo = val.GradeChangeTo,
                               EffDate = val.EffDate
                           }).FirstOrDefault();
            return lstdata;
        }
    }
}
