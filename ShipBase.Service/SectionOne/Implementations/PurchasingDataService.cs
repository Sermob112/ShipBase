using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.DAL.SectionOne.Repositories;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Enum;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Customer;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using ShipBase.Service.SectionOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Service.SectionOne.Implementations
{
    public class PurchasingDataService : IPurchasingDataService
    {

        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IBaseRepository<PurchasingData> _purchasingDataRepository;
        public PurchasingDataService(IBaseRepository<Customer> customerRepository, IBaseRepository<PurchasingData> purchasingDataRepository)
        {
            _customerRepository = customerRepository;
            _purchasingDataRepository = purchasingDataRepository;
        }



        public async Task<IBaseResponse<PurchasingData>> Create(PurchasingDataCreateViewModel purch)
        {
            try
            {
                var user = await _purchasingDataRepository.GetAll().FirstOrDefaultAsync(x => x.Id == purch.Id);
                if (user != null)
                {
                    return new BaseResponse<PurchasingData>()
                    {
                        Description = "Закупка с таким номером уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }


                var cust = await _customerRepository.GetAll()
                   .Include(x => x.PurchasingData)
                   .FirstOrDefaultAsync(x => x.purchasing_id == purch.Id);
                var data = new PurchasingData()
                {
                    Id = purch.Id,
                    Purchase_stage = purch.Purchase_stage,
                    Num_Of_Applications = purch.Num_Of_Applications,
                    Method_of_purchasing = purch.Method_of_purchasing,
                    Start_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    End_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    NMCK = Convert.ToInt32(purch.NMCK),
                    Federal_law = Convert.ToInt32(purch.Federal_law),
                    Num_of_ships = Convert.ToInt32(purch.Num_of_ships),
                    Purchase_object = purch.Purchase_object,

                };
                await _purchasingDataRepository.Create(data);

                return new BaseResponse<PurchasingData>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PurchasingData>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<PurchasingData>> Create(PurchasingDataViewModel purch)
        {
            try
            {
                var user = await _purchasingDataRepository.GetAll().FirstOrDefaultAsync(x => x.Id == purch.Id);
                if (user != null)
                {
                    return new BaseResponse<PurchasingData>()
                    {
                        Description = "Закупка с таким номером уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }


                var cust = await _customerRepository.GetAll()
                   .Include(x => x.PurchasingData)
                   .FirstOrDefaultAsync(x => x.purchasing_id == purch.Id);
                var data = new PurchasingData()
                {
                    Id = purch.Id,
                    Purchase_stage = purch.Purchase_stage,
                    Num_Of_Applications = purch.Num_Of_Applications,
                    Method_of_purchasing = purch.Method_of_purchasing,
                    Start_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    End_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    NMCK = Convert.ToInt32(purch.NMCK),
                    Federal_law = Convert.ToInt32(purch.Federal_law),
                    Num_of_ships = Convert.ToInt32(purch.Num_of_ships),
                    Purchase_object = purch.Purchase_object,
                 
                };
                await _purchasingDataRepository.Create(data);

                return new BaseResponse<PurchasingData>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PurchasingData>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var purch = await _purchasingDataRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Purchase not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _purchasingDataRepository.Delete(purch);

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
                    Description = $"[Delete] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PurchasingData>> Edit(long id, PurchasingDataViewModel model)
        {
            try
            {
                var purch = await _purchasingDataRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<PurchasingData>()
                    {
                        Description = "Purchasing Data not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                purch.Id = model.Id;
                purch.Purchase_stage = model.Purchase_stage;
                purch.Num_Of_Applications = model.Num_Of_Applications;
                purch.Method_of_purchasing = model.Method_of_purchasing;
                purch.Start_data = DateTime.SpecifyKind(model.Start_data.LocalDateTime, DateTimeKind.Utc);
                purch.End_data = DateTime.SpecifyKind(model.Start_data.LocalDateTime, DateTimeKind.Utc);
                purch.NMCK = Convert.ToInt32(model.NMCK);
                purch.Federal_law = Convert.ToInt32(model.Federal_law);
                purch.Num_of_ships = Convert.ToInt32(model.Num_of_ships);
                purch.Purchase_object = model.Purchase_object;

                await _purchasingDataRepository.Update(purch);


                return new BaseResponse<PurchasingData>()
                {
                    Data = purch,
                    StatusCode = StatusCode.OK,
                };
               
            }
            catch (Exception ex)
            {
                return new BaseResponse<PurchasingData>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PurchasingDataViewModel>> GetPurchasingData(long id)
        {
            var cust = await _customerRepository.GetAll()
                  .Include(x => x.PurchasingData)
                  .FirstOrDefaultAsync(x => x.purchasing_id == id);
            var customers = await _customerRepository.GetAll()
         .Include(c => c.PurchasingData)
         .ToListAsync();
          


            try
            {
                var purch = await _purchasingDataRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<PurchasingDataViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PurchasingDataViewModel()
                {
                    Id = purch.Id,
                    Purchase_stage = purch.Purchase_stage,
                    Num_Of_Applications = purch.Num_Of_Applications,
                    Method_of_purchasing = purch.Method_of_purchasing,
                    Start_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    End_data = DateTime.SpecifyKind(purch.Start_data.LocalDateTime, DateTimeKind.Utc),
                    NMCK = Convert.ToInt32(purch.NMCK),
                    Federal_law = Convert.ToInt32(purch.Federal_law),
                    Num_of_ships = Convert.ToInt32(purch.Num_of_ships),
                    Purchase_object = purch.Purchase_object,
                  

                };

                return new BaseResponse<PurchasingDataViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PurchasingDataViewModel>()
                {
                    Description = $"[GetPurchasingData] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public  async Task<BaseResponse<Dictionary<long, string>>> GetPurchasingData(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var purch = await _purchasingDataRepository.GetAll()
                    .Select(x => new PurchasingDataViewModel()
                    {
                        Id = x.Id,
                        Purchase_stage = x.Purchase_stage,
                        Num_Of_Applications = x.Num_Of_Applications,
                        Method_of_purchasing = x.Method_of_purchasing,
                        Start_data = DateTime.SpecifyKind(x.Start_data.LocalDateTime, DateTimeKind.Utc),
                        End_data = DateTime.SpecifyKind(x.Start_data.LocalDateTime, DateTimeKind.Utc),
                        NMCK = Convert.ToInt64(x.NMCK),
                        Federal_law = Convert.ToInt32(x.Federal_law),
                        Num_of_ships = Convert.ToInt32(x.Num_of_ships),
                        Purchase_object = x.Purchase_object,
                    })
                    .Where(x => EF.Functions.Like(x.Purchase_object, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Purchase_object);

                baseResponse.Data = purch;
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

        public IBaseResponse<List<PurchasingData>> GetPurchasingDatas()
        {
           try
            {

                var purch = _purchasingDataRepository.GetAll().ToList();
                if (!purch.Any())
                {
                    return new BaseResponse<List<PurchasingData>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                
                return new BaseResponse<List<PurchasingData>>()
                {
                    Data = purch,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PurchasingData>>()
                {
                    Description = $"[GetPurchasingDatas] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

       /* public async Task<IBaseResponse<List<PurchasingDataViewModel>>> GetPurchasingDatasView()
        {
            try
            {

                var customers = await _customerRepository.GetAll()
               .Include(c => c.PurchasingData)
               .ToListAsync();
                var purch = await _purchasingDataRepository.GetAll()
                    .Select(x => new PurchasingDataViewModel()
                    
                    })
                    .ToListAsync();


                if (!customers.Any())
                {
                    return new BaseResponse<List<PurchasingDataViewModel>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<PurchasingDataViewModel>>()
                {
                    Data = purch,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PurchasingDataViewModel>>()
                {
                    Description = $"[GetPurchasingDatas] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }*/
    }
}
