using Microsoft.EntityFrameworkCore;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Enum;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Customer;
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

        private readonly IBaseRepository<Customer> _customerRepository;

        public CustomerService(IBaseRepository<Customer> CustomerRepository)
        {
            _customerRepository = CustomerRepository;
        }
        public async Task<IBaseResponse<Customer>> Create(СustomerViewModel customer)
        {
            try
            {
                var cust = new Customer()
                {
                    Name_of_organization = customer.Name_of_organization,
                    OGRN = customer.OGRN,
                    INN = customer.INN,
                    KPP = customer.KPP,
                    purchasing_id = customer.purchasing_id
                   
                };
                await _customerRepository.Create(cust);

                return new BaseResponse<Customer>()
                {
                    StatusCode = StatusCode.OK,
                    Data = cust
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public async Task<IBaseResponse<bool>> DeleteCustomer(long id)
        {
            try
            {
                var cust = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (cust == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Customer not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _customerRepository.Delete(cust);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Customer>> Edit(long id, СustomerViewModel model)
        {
            try
            {
                var cust = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (cust == null)
                {
                    return new BaseResponse<Customer>()
                    {
                        Description = "Сustomer not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                cust.Name_of_organization = model.Name_of_organization;
                cust.OGRN = long.Parse(model.OGRN.ToString());
                cust.INN = model.INN;
                cust.KPP = model.KPP;
                cust.purchasing_id = model.purchasing_id;


                await _customerRepository.Update(cust);


                return new BaseResponse<Customer>()
                {
                    Data = cust,
                    StatusCode = StatusCode.OK,
                };
               
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<СustomerViewModel>> GetCustomer(long id)
        {
            try
            {
                var cust = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (cust == null)
                {
                    return new BaseResponse<СustomerViewModel>()
                    {
                        Description = "заказчик не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new СustomerViewModel()
                {
                Name_of_organization = cust.Name_of_organization,
                OGRN = cust.OGRN,
                INN = cust.INN,
                KPP = cust.KPP,
                purchasing_id = cust.purchasing_id,

            };

                return new BaseResponse<СustomerViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<СustomerViewModel>()
                {
                    Description = $"[GetCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Customer>> GetCustomers()
        {
            try
            {
                var cust = _customerRepository.GetAll().ToList();
                if (!cust.Any())
                {
                    return new BaseResponse<List<Customer>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Customer>>()
                {
                    Data = cust,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Customer>>()
                {
                    Description = $"[GetCustomers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<Dictionary<long, string>>> GetCustomer(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var cust = await _customerRepository.GetAll()
                    .Select(x => new СustomerViewModel()
                    {
                        Id = x.Id,
                        INN = x.INN,
                        Name_of_organization = x.Name_of_organization,
                        OGRN = x.OGRN,
                        KPP = x.KPP,
                        purchasing_id = x.purchasing_id,
                       
                    })
                    .Where(x => EF.Functions.Like(x.Name_of_organization, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name_of_organization);

                baseResponse.Data = cust;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
