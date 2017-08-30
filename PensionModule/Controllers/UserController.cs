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

        public ActionResult Index()
        {
            LoginModel objModel = new LoginModel();
            LoginService objService = new LoginService();
            List<LoginModel> lstLogin = new List<LoginModel>();

            lstLogin = objService.getUser();
            objModel.LoginList = new List<LoginModel>();
            objModel.LoginList.AddRange(lstLogin);

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            LoginService objService = new LoginService();
            objService.Insert(model);

            return View();
        }

    }
}
