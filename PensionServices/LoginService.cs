using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionServices
{
    public interface ILoginService
    {
        List<LoginModel> getUser();
        int Insert(LoginModel model);
        LoginModel GetDetails(string UserName, String Password);
    }

    public class LoginService : ILoginService
    {
        VPDPensionEntities DBContext = new VPDPensionEntities();
        public List<LoginModel> getUser()
        {
            try
            {
                var data = (from um in DBContext.LoginMasters
                            join rm in DBContext.RoleMasters on um.RID equals rm.RID
                            select new LoginModel()
                            {
                                UID = um.UID,
                                RID = um.RID,
                                Name = um.Name,
                                Status = um.Status,
                                UserID = um.UserID,
                                Password = um.Password,
                                Mobile = um.Mobile,
                                Email = um.Email,
                                Address = um.Address,
                                CreatedBy = um.CreatedBy,
                                CreatedDate = um.CreatedDate,
                                UpdatedBy = um.UpdatedBy,
                                UpdatedDate = um.UpdatedDate,
                                RoleDetails = new RoleModel()
                                {
                                    Role = rm.Role
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RoleModel> getRole()
        {
            try
            {
                Mapper.CreateMap<RoleMaster, RoleModel>();
                List<RoleMaster> tblMaster = DBContext.RoleMasters.Where(m => m.Status == true).ToList();
                List<RoleModel> lstmasterdata = Mapper.Map<List<RoleModel>>(tblMaster);
                return lstmasterdata;


            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("UserMaster", "LoginService", "getRole", ex);
                return null;
            }


        }
        public int Insert(LoginModel model)
        {
            try
            {
                Mapper.CreateMap<LoginModel, LoginMaster>();
                LoginMaster objUser = Mapper.Map<LoginMaster>(model);
                DBContext.LoginMasters.Add(objUser);
                DBContext.SaveChanges();

                int uid = DBContext.LoginMasters.Max(m => m.UID);
                return uid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public LoginModel GetDetails(string UserName, String Password)
        {
            Mapper.CreateMap<LoginMaster, LoginModel>();
            LoginMaster Login = DBContext.LoginMasters.FirstOrDefault(m => m.UserID == UserName && m.Password == Password);
            LoginModel objLogin = Mapper.Map<LoginModel>(Login);
            return objLogin;
        }
        public LoginModel GetById(int id)
        {
            Mapper.CreateMap<LoginMaster, LoginModel>();
            LoginMaster objBranch = DBContext.LoginMasters.SingleOrDefault(m => m.UID == id);
            LoginModel objBItem = Mapper.Map<LoginModel>(objBranch);
            return objBItem;            
        }
        public int Update(LoginModel model)
        {
            Mapper.CreateMap<LoginModel, LoginMaster>();
            LoginMaster objBranch = DBContext.LoginMasters.SingleOrDefault(m => m.UID == model.UID);
            objBranch = Mapper.Map(model, objBranch);
            return DBContext.SaveChanges();
        }
    }
}
