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
    public class IncentiveServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<IncentiveViewModel> lstdata()
        {
            var lstdata = (from val in DbContext.Incentives
                           select new IncentiveViewModel()
                           {
                               Id = val.Id,
                               From = val.From,
                               To = val.To,
                               Yly = val.Yly,
                               Hly = val.Hly,
                               Qly = val.Qly,
                               Mly = val.Mly,
                               EffDate = val.EffDate,
                               Status = val.Status

                           }).ToList();
            return lstdata;
        }

        public int InsertUpdate(IncentiveViewModel model, int flag)
        {
            if (flag == 1)
            {
                Mapper.CreateMap<IncentiveViewModel, Incentive>();
                Incentive lstincen = Mapper.Map<Incentive>(model);
                DbContext.Incentives.Add(lstincen);
                return DbContext.SaveChanges();
            }
            else
            {
                model.Id = model.hdnin;
                Mapper.CreateMap<IncentiveViewModel, Incentive>();
                Incentive lstincen = DbContext.Incentives.SingleOrDefault(m => m.Id == model.hdnin);
                lstincen = Mapper.Map(model, lstincen);
                return DbContext.SaveChanges();
            }
        }

        public IncentiveViewModel GetByID(int id)
        {
            var lstdata = (from val in DbContext.Incentives
                           where val.Id == id
                           select new IncentiveViewModel()
                           {
                               Id = val.Id,
                               From = val.From,
                               To = val.To,
                               Yly = val.Yly,
                               Hly = val.Hly,
                               Qly = val.Qly,
                               Mly = val.Mly,
                               EffDate = val.EffDate,
                               Status = val.Status

                           }).SingleOrDefault();
            return lstdata;
        }

        public int status(IncentiveViewModel model)
        {
            Incentive objin = new Incentive();
            objin = DbContext.Incentives.Where(m => m.Id == model.Id).SingleOrDefault();
            if (model.Status == 0)
            {
                objin.Status = 1;
            }
            else
            {
                objin.Status = 0;
            }
            return DbContext.SaveChanges();
        }
    }
}
