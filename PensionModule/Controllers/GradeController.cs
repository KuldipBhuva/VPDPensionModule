using Pension.Models.ViewModel;
using Pension.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pension.Controllers
{
    public class GradeController : Controller
    {
        //
        // GET: /Grade/
        GradeServices services = new GradeServices();
        public readonly IGradeServices gradeService;
        //Any variable declared globally need to be readonly and create new instance in constructor.


        private readonly List<GradeViewModel> lstgrade;//= new List<GradeViewModel>();

        public GradeController()
        {
            //This would be the case to follow... which will be good to replace dependency injection

            lstgrade = new List<GradeViewModel>();

            gradeService = new GradeServices(); 
            //this.gradeService = gradeService;
        }

        public ActionResult Grade()
        {
            try
            {
                GradeViewModel model = new GradeViewModel();
                //lstgrade = new List<GradeViewModel>();
                model.lstgrade = services.lstGrade();
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", " GradeController", "Grade", ex);
                return null;
            }
        }
        [HttpPost]
        public ActionResult Grade(GradeViewModel model, string Button)
        {
            //lstgrade = new List<GradeViewModel>();
            try
            {
                switch (Button)
                {
                    case "Save":
                        if (model.hdnGradeID != 0)
                        {
                            int Update = services.UpdateGrade(model.hdnGradeID, model);
                        }
                        else
                        {
                            int a = services.InsertGrade(model);
                        }
                        model.lstgrade = services.lstGrade();
                        return View(model);

                    default:
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", " GradeController", "Grade", ex);
                return null;
            }

        }

        public JsonResult GetDataById(int id)
        {
            try
            {
                services = new GradeServices();
                GradeViewModel CompModel = new GradeViewModel();
                CompModel = services.GetById(id);
                return Json(CompModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", " GradeController", "GetDataById", ex);
                return null;
            }
        }

        public JsonResult CheckExists(GradeViewModel model)
        {
            services = new GradeServices();
            GradeViewModel objLangModel = new GradeViewModel();
            objLangModel = services.CheckExists(model.Grade_Name);

            return Json(objLangModel, JsonRequestBehavior.AllowGet);
        }

    }
}

