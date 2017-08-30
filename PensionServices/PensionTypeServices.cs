using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using Pension.Models.ViewModel;
using AutoMapper;

namespace PensionServices
{
    public class PensionTypeServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<PensionTypeViewModel> lstdata()
        {
            try
            {
                var lstdata = (from val in DbContext.PensionTypes
                               select new PensionTypeViewModel()
                               {
                                   Id = val.Id,
                                   Type = val.Type,
                                   Description = val.Description,
                                   Status = val.Status
                               }).ToList();
                return lstdata;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int insert(PensionTypeViewModel model, int flag)
        {
            try
            {
                if (flag == 1)
                {
                    Mapper.CreateMap<PensionTypeViewModel, PensionType>();
                    PensionType objpen = Mapper.Map<PensionType>(model);
                    DbContext.PensionTypes.Add(objpen);
                    return DbContext.SaveChanges();
                }
                else
                {
                    model.Id = model.hdnpentype;
                    Mapper.CreateMap<PensionTypeViewModel, PensionType>();
                    PensionType objpen = DbContext.PensionTypes.SingleOrDefault(m => m.Id == model.hdnpentype);
                    objpen = Mapper.Map(model, objpen);
                    return DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public PensionTypeViewModel GetByID(int id)
        {
            Mapper.CreateMap<PensionType, PensionTypeViewModel>();
            PensionType objpensiontype = DbContext.PensionTypes.Where(m => m.Id == id).SingleOrDefault();
            PensionTypeViewModel objpension = Mapper.Map<PensionTypeViewModel>(objpensiontype);
            return objpension;
        }
    }
}
