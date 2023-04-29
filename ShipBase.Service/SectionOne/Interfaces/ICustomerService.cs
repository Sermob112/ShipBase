using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.ViewModels;
using ShipBase.Domain.SectionOne.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

using ShipBase.Domain.SectionOne.ViewModels.Customer;

namespace ShipBase.Service.SectionOne.Interfaces
{
    public interface ICustomerService
    {
        
        IBaseResponse<List<Customer>> GetCustomers();
        Task<IBaseResponse<СustomerViewModel>> GetCustomer(long id);
        Task<BaseResponse<Dictionary<long, string>>> GetCustomer(string term);
        Task<IBaseResponse<Customer>> Create(СustomerViewModel customer);
        Task<IBaseResponse<bool>> DeleteCustomer(long id);

        Task<IBaseResponse<Customer>> Edit(long id, СustomerViewModel model);


    }
}
