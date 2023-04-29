using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Customer;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Service.SectionOne.Interfaces
{
    public interface IPurchasingDataService
    {
        IBaseResponse<List<PurchasingData>> GetPurchasingDatas();
        Task<IBaseResponse<PurchasingDataViewModel>> GetPurchasingData(long id);
        Task<BaseResponse<Dictionary<long, string>>> GetPurchasingData(string term);
        Task<IBaseResponse<PurchasingData>> Create(PurchasingDataViewModel customer);
        Task<IBaseResponse<bool>> Delete(long id);

        Task<IBaseResponse<PurchasingData>> Edit(long id, PurchasingDataViewModel model);


    }
}
