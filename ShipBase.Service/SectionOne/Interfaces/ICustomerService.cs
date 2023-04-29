using ShipBase.Domain.Entity;
using ShipBase.Domain.ViewModels;
using ShipBase.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

using ShipBase.Domain.ViewModels.Customer;

namespace ShipBase.Service.SectionOne.Interfaces
{
    public interface ICustomerService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        IBaseResponse<List<Сustomer>> GetCustomers();
        Task<IBaseResponse<СustomerViewModel>> GetCustomer(long id);
        Task<IBaseResponse<Сustomer>> Create(СustomerViewModel customer);
        Task<IBaseResponse<bool>> DeleteCustomer(long id);

        Task<IBaseResponse<Сustomer>> Edit(long id, СustomerViewModel model);


    }
}
