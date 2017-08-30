using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PensionModel;
using PensionModel.ViewModel;

namespace PensionServices
{
    public class PaymentTypeServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();


        public List<PaymentTypeModel> lstpaytype()
        {
            var lstpaytype = (from pay in DbContext.PaymentTypes
                              select new PaymentTypeModel()
                              {
                                  id = pay.id,
                                  PaymentType1 = pay.PaymentType1,
                                  OtherCharge = pay.OtherCharge,
                                  Description = pay.Description,
                                  Status = pay.Status
                              }).ToList();
            return lstpaytype;
        }


        public int insert(PaymentTypeModel model, int flag)
        {
            if (flag == 1)
            {
                Mapper.CreateMap<PaymentTypeModel, PaymentType>();
                PaymentType objpaytype = Mapper.Map<PaymentType>(model);
                DbContext.PaymentTypes.Add(objpaytype);
                return DbContext.SaveChanges();
            }
            else
            {
                model.id = model.hdnpayid;
                Mapper.CreateMap<PaymentTypeModel, PaymentType>();
                PaymentType objpaytype = DbContext.PaymentTypes.FirstOrDefault(m => m.id == model.hdnpayid);
                objpaytype = Mapper.Map(model, objpaytype);
                return DbContext.SaveChanges();
            }
        }

        public PaymentTypeModel GetByID(int id)
        {
            Mapper.CreateMap<PaymentType, PaymentTypeModel>();
            PaymentType objpaytype = DbContext.PaymentTypes.Where(m => m.id == id).SingleOrDefault();
            PaymentTypeModel objpay = Mapper.Map<PaymentTypeModel>(objpaytype);
            return objpay;
        }
    }
}
