using AutoMapper;
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
    }
}
