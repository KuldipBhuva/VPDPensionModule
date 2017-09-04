using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        LoginModel objModel = new LoginModel();
        LoginService objService = new LoginService();
        public ActionResult Index()
        {
            
            List<LoginModel> lstLogin = new List<LoginModel>();
            
            lstLogin = objService.getUser();
            objModel.LoginList = new List<LoginModel>();
            objModel.LoginList.AddRange(lstLogin);

            List<RoleModel> lstRole = new List<RoleModel>();
            lstRole=objService.getRole();
            objModel.ListRole = new List<RoleModel>();
            objModel.ListRole.AddRange(lstRole);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objService.Insert(model);

            return View();
        }
        public ActionResult Edit(int id)
        {
            
            
            objModel = objService.GetById(id);
            List<RoleModel> lstRole = new List<RoleModel>();
            lstRole = objService.getRole();
            objModel.ListRole = new List<RoleModel>();
            objModel.ListRole.AddRange(lstRole);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Edit(LoginModel model)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            if (model.RID == 1 && model.RID == 2)
            {
                model.RID = null;
            }
            model.Status = true;
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objService.Update(model);
            return RedirectToAction("Index");
        }
    }
}
