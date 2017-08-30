using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Pension.Services;
using Pension.Models;
using PensionModel;
namespace Pension.Services
{

    public interface IGradeServices
    {
        List<GradeViewModel> lstGrade();
        int InsertGrade(GradeViewModel model);
        GradeViewModel GetById(int id);
        int UpdateGrade(int Id, GradeViewModel model);
        GradeViewModel CheckExists(string Grade_Name);
    }

    public class GradeServices : IGradeServices, IDisposable
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<GradeViewModel> lstGrade()
        {
            try
            {
                var lstEmployeer = (from grd in DbContext.GradeMasters
                                    select new GradeViewModel()
                                    {
                                        id = grd.id,
                                        Grade_Name = grd.Grade_Name,
                                        Description = grd.Description,
                                        Status = grd.Status


                                    }).ToList();
                return lstEmployeer;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "lstGrade", ex);
                return null;
            }
        }

        public int InsertGrade(GradeViewModel model)
        {
            try
            {
                GradeMaster objGrade = new GradeMaster();
                objGrade.Grade_Name = model.Grade_Name;
                objGrade.Description = model.Description;
                objGrade.Status = model.Status;

                DbContext.GradeMasters.Add(objGrade);
                DbContext.SaveChanges();
                return objGrade.id;

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "InsertGrade", ex);
                return 0;
            }


        }


        public GradeViewModel GetById(int id)
        {
            try
            {
                var lstGrd = (from grd in DbContext.GradeMasters
                              where grd.id == id
                              select new GradeViewModel()
                              {
                                  id = grd.id,
                                  Grade_Name = grd.Grade_Name,
                                  Description = grd.Description,
                                  Status = grd.Status

                              }).FirstOrDefault();

                return lstGrd;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "GetById", ex);
                return null;
            }



        }

        public int UpdateGrade(int Id, GradeViewModel model)
        {
            try
            {
                GradeMaster objGrade = new GradeMaster();
                objGrade = DbContext.GradeMasters.SingleOrDefault(m => m.id == Id);
                objGrade.Grade_Name = model.Grade_Name;
                objGrade.Description = model.Description;
                objGrade.Status = model.Status;

                return DbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "UpdateGrade", ex);
                return 0;
            }

        }

        public GradeViewModel CheckExists(string Grade_Name)
        {
            try
            {
                var CompData = (from comp in DbContext.GradeMasters
                                where comp.Grade_Name == Grade_Name
                                select new GradeViewModel()
                                {
                                    Grade_Name = comp.Grade_Name
                                }).SingleOrDefault();

                return CompData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "GradeController", "UpdateGrade", ex);
                return null;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }


    }
}