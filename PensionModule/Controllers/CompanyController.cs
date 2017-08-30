﻿using Pension.Models.ViewModel;
using Pension.Services;
using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PensionModel;

namespace PensionModule.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        CompanyServices objService = new CompanyServices();
        CompanyModel objModel = new CompanyModel();
        List<CompanyModel> lstComp = new List<CompanyModel>();
        int cmy = 0;
        int said = 0;
        public ActionResult Index()
        {
            lstComp = objService.lstCompany();
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            List<CompanyModel> lstCmy = new List<CompanyModel>();
            if (Session["Comp"] != null)
            {
                cmy = Convert.ToInt32(Session["Comp"].ToString());
            }
            lstCmy = objService.lstCmy(cmy);
            objModel.LstCmy = new List<CompanyModel>();
            objModel.LstCmy.AddRange(lstCmy);

            //objModel.ListSA = objService.lstSAInt(cmy);
            objModel.ListPension = objService.GetPenByCmyId(cmy);
            objModel.ListMergeComp = objService.GetMergerByCmyId(cmy);
            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;

            return View(objModel);
        }

        public void bindgrade()
        {
            try
            {
                GradeServices insservices = new GradeServices();
                List<GradeViewModel> lstgrade = new List<GradeViewModel>();

                lstgrade = insservices.lstGrade();
                objModel.Listgrade = new List<GradeViewModel>();
                objModel.Listgrade.AddRange(lstgrade);
                ViewBag.Listgrade = objModel.Listgrade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Index(CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel)
        {
            model.Status = 1;
            mergemodel.EffDate = model.MergeEffDate;
            penmodel.EffDate = model.PenEffDate;
            objService.InsertCompany(model, penmodel, mergemodel, samodel);
            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;

            objModel.ListSA = objService.lstSAInt(cmy);
            lstComp = objService.lstCompany();
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            return View(objModel);
        }

        public ActionResult Edit(int id)
        {
            try
            {
                Session["Comp"] = id;
                objModel = objService.GetById(id);

                List<CompanyModel> lstCmy = new List<CompanyModel>();
                lstCmy = objService.lstCmy(id);
                objModel.LstCmy = new List<CompanyModel>();
                objModel.LstCmy.AddRange(lstCmy);

                objModel.ListSA = objService.lstSAInt(id);
                objModel.ListPension = objService.GetPenByCmyId(id);
                objModel.ListMergeComp = objService.GetMergerByCmyId(id);
                bindgrade();
                objModel.Listgrade = ViewBag.Listgrade;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyMaster", "Edit", ex);
                return null;
            }
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel)
        {
            try
            {
                mergemodel.EffDate = model.MergeEffDate;
                penmodel.EffDate = model.PenEffDate;
                int SaID = Convert.ToInt32(Session["SaID"]);
                objService.UpdateCompanyData(model.id, SaID, model, penmodel, mergemodel, samodel);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyMaster", "Update", ex);
            }
            int id = Convert.ToInt32(Session["Comp"].ToString());
            List<CompanyModel> lstCmy = new List<CompanyModel>();
            lstCmy = objService.lstCmy(id);
            objModel.LstCmy = new List<CompanyModel>();
            objModel.LstCmy.AddRange(lstCmy);

            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;
            objModel.ListPension = objService.GetPenByCmyId(id);
            objModel.ListMergeComp = objService.GetMergerByCmyId(id);
            objModel.ListSA = objService.lstSAInt(id);

            return View(objModel);
        }

        [HttpPost]
        public ActionResult ChangeComp(FormCollection All)
        {

            int CompID = Convert.ToInt32(All["dist"]);
            Session["Comp"] = CompID;
            Session["Emp"] = null;
            TempData["AMsg"] = "Company set successfully.";
            //string currentPath = HttpContext.Request.UrlReferrer.AbsolutePath;

            //Response.Redirect(currentPath);
            //return Content("<script language='javascript' type='text/javascript'>alert('District changed successfully.');</script>");
            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        public JsonResult GetPension(CompanyModel model)
        {
            int cmyid = Convert.ToInt32(Session["Comp"].ToString());
            List<CompanyModel> lstCmy = new List<CompanyModel>();
            lstCmy = objService.lstCmy(cmyid);
            objModel.LstCmy = new List<CompanyModel>();
            objModel.LstCmy.AddRange(lstCmy);

            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;
            int penid = Convert.ToInt32(model.PenId);
            objModel = objService.GetPenById(cmyid, penid);

            if (objModel == null)
            {
                objModel = objService.GetById(cmyid);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMCmy(CompanyModel model)
        {
            int cmyid = Convert.ToInt32(Session["Comp"].ToString());
            List<CompanyModel> lstCmy = new List<CompanyModel>();
            lstCmy = objService.lstCmy(cmyid);
            objModel.LstCmy = new List<CompanyModel>();
            objModel.LstCmy.AddRange(lstCmy);

            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;
            int merid = Convert.ToInt32(model.MerId);
            objModel = objService.GetMergerById(cmyid, merid);

            if (objModel == null)
            {
                objModel = objService.GetById(cmyid);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSaInt(CompanyModel model)
        {
            int cmyid = Convert.ToInt32(Session["Comp"].ToString());
            List<CompanyModel> lstCmy = new List<CompanyModel>();
            lstCmy = objService.lstCmy(cmyid);
            objModel.LstCmy = new List<CompanyModel>();
            objModel.LstCmy.AddRange(lstCmy);

            bindgrade();
            objModel.Listgrade = ViewBag.Listgrade;
            said = Convert.ToInt32(model.SaID);
            objModel = objService.GetSaById(cmyid, said);
            Session["SaID"] = said;
            if (objModel == null)
            {
                objModel = objService.GetById(cmyid);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
    }
}