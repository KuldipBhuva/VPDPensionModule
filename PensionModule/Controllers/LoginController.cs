using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            LoginService objService = new LoginService();
            LoginModel objmodel= objService.GetDetails(model.UserID, model.Password);
            if (objmodel != null)
            {
                Session["UID"] = objmodel.UID;
                Session["UserName"] = objmodel.UserID;
                Session["Password"] = objmodel.Password;
                Session["RID"] = objmodel.RID;
                Session["Comp"] = objmodel.CompID;

                if (objmodel.RID == 3)
                {
                    return RedirectToAction("Index","Employee");
                }
                else
                {
                    return RedirectToAction("Index", "Company");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Check Credential");
                return View();
            }
            
        }
        public ActionResult Logout()
        {

            Session["UId"] = null;
            Session["UserName"] = null;
            Session["Password"] = null;
            Session["RID"] = null;

            Session["Comp"] = null;
            Session["Emp"] = null;
            return View("Index");
        }
    }
}
