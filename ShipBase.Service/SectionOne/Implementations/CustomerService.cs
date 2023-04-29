using ShipBase.DAL.Interfaces;
using ShipBase.Domain.Entity;
using ShipBase.Domain.Enum;
using ShipBase.Domain.Response;
using ShipBase.Domain.ViewModels.Customer;
using ShipBase.Service.SectionOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Service.SectionOne.Implementations
{
    public class CustomerService : ICustomerService
    {

        private readonly IBaseRepository<Сustomer> _customerRepository;

        public CustomerService(IBaseRepository<Сustomer> CustomerRepository)
        {
            _customerRepository = CustomerRepository;
        }
        public async Task<IBaseResponse<Сustomer>> Create(СustomerViewModel customer)
        {
            try
            {
                var cust = new Сustomer()
                {
                    Name_of_organization = customer.Name_of_organization,
                    OGRN = customer.OGRN,
                    INN = customer.INN,
                    KPP = customer.KPP,
                    purchasing_id = customer.purchasing_id
                   
                };
                await _customerRepository.Create(cust);

                return new BaseResponse<Сustomer>()
                {
                    StatusCode = StatusCode.OK,
                    Data = cust
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Сustomer>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public Task<IBaseResponse<bool>> DeleteCustomer(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<Сustomer>> Edit(long id, СustomerViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<СustomerViewModel>> GetCustomer(long id)
        {
            throw new NotImplementedException();
        }

        public IBaseResponse<List<Сustomer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            throw new NotImplementedException();
        }
    }
}
