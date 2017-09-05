using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using Pension.Services;
using System.IO;

namespace PensionModule.Controllers
{
    public class LifeCertificateController : Controller
    {
        //
        // GET: /LifeCertificate/

        LifeCertificateViewModel lstmodel = new LifeCertificateViewModel();
        LifeCertificateServices lstservices = new LifeCertificateServices();

        public ActionResult LifeCertificate()
        {
            if (Session["Comp"] != null)
            {
                if (Session["Emp"] != null)
                {
                    try
                    {
                        int id = Convert.ToInt32(Session["Emp"]);
                        int compid = Convert.ToInt32(Session["Comp"]);
                        lstmodel.lstlife = lstservices.lstdata(id, compid);
                        //get(id);
                        return View(lstmodel);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    return RedirectToAction("../Employee/Index");
                }
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }
        }

        public void get(int compid)
        {
            try
            {
                //CompanyServices objService = new CompanyServices();
                List<LifeCertificateViewModel> listEmp = new List<LifeCertificateViewModel>();
                //listEmp = objService.GetById(compid);
                lstmodel.lstlife = new List<LifeCertificateViewModel>();
                lstmodel.lstlife.AddRange(listEmp);
                ViewBag.lstlife = lstmodel.lstlife;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult LifeCertificate(LifeCertificateViewModel model)
        {
            try
            {
                if (model.Value != "no")
                {
                    model.EmpId = Convert.ToInt32(Session["Emp"].ToString());
                    model.InsCompId = Convert.ToInt32(Session["Comp"].ToString());
                    model.EffDate = System.DateTime.Now;
                    string date = "12/31/" + model.YearCode + "";
                    model.ToDate = Convert.ToDateTime(date);

                    HttpPostedFileBase file = Request.Files["file"];
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName != "")
                        {
                            var path = Path.Combine(Server.MapPath("../Upload/CertificateImg/"), fileName);
                            file.SaveAs(path);
                            model.CertificateCopy = "../Upload/CertificateImg/" + fileName;
                        }
                        if (model.hdnlife == 0)
                        {
                            int a = lstservices.insert(model, 1);
                        }
                    }
                    if (model.hdnlife != 0)
                    {
                        if (model.CertificateCopy == null)
                        {
                            model.CertificateCopy = Convert.ToString(TempData["Certificate"]);
                        }
                        int b = lstservices.insert(model, 2);
                    }
                    int id = Convert.ToInt32(Session["Emp"]);
                    int compid = Convert.ToInt32(Session["Comp"]);
                    lstmodel.lstlife = lstservices.lstdata(id, compid);
                    return View(lstmodel);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckExists(LifeCertificate model)
        {
            int id = Convert.ToInt32(Session["Emp"]);
            LifeCertificateViewModel objLangModel = new LifeCertificateViewModel();
            objLangModel = lstservices.CheckExists(model.YearCode, id);

            return Json(objLangModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetByID(LifeCertificateViewModel model)
        {
            try
            {
                lstmodel.EmpId = Convert.ToInt32(Session["Emp"]);
                lstmodel = lstservices.GetByID(model.id);
                TempData["Certificate"] = lstmodel.CertificateCopy;
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
