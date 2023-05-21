using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.DAL.SectionOne.Repositories;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Enum;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Purch;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using ShipBase.Service.SectionOne.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Service.SectionOne.Implementations
{
    public class PurchService : IPurchService
    {

        private readonly IBaseRepository<Purch> _purchRepository;

        public PurchService(IBaseRepository<Purch> purchRepository)
        {
            _purchRepository = purchRepository;

        }

        public async Task<IBaseResponse<Purch>> Create(PurchViewModel purch)
        {
            try
            {
                var user = await _purchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == purch.Id);
                if (user != null)
                {
                    return new BaseResponse<Purch>()
                    {
                        Description = "Закупка с таким номером уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }


               
                var data = new Purch()
                {
                    Federal_law = purch.Federal_law,
                    Id = purch.Id,
                    Method_of_purchasing = purch.Method_of_purchasing,
                    Purchase_object = purch.Purchase_object,
                    NMCK = Convert.ToDouble(purch.NMCK),
                    Name_of_customer = purch.Name_of_customer,
                    Hosting_organization = purch.Hosting_organization,
                    Set_data = purch.Set_data,
                    Update_data = purch.Update_data,
                    Purchase_stage = purch.Purchase_stage,
                    Features_of_placing = purch.Features_of_placing,
                    Start_data = purch.Start_data,
                    End_data = purch.End_data,
                    Date_of_electronic_auction = purch.Date_of_electronic_auction,

                };
                await _purchRepository.Create(data);

                return new BaseResponse<Purch>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Purch>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

      

        public async Task<IBaseResponse<Purch>> CreateFromFile(IFormFile filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                var data = new Purch();
                using (StreamReader reader = new StreamReader(filePath.OpenReadStream(), Encoding.GetEncoding("Windows-1251")))
                {
                    string line;
                    bool isFirstLine = true;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue; // Пропускаем первую строку
                        }

                        string[] values = line.Split(';');

                        data = new Purch();

                       
                        data.Federal_law = values[0];
                        data.Id = long.Parse(values[1].Replace("№", ""));
                        data.Method_of_purchasing = values[2];
                        try
                        {
                            data.Purchase_object = values[3];
                            data.NMCK = double.Parse(values[8].Replace(".", ","));
                            data.Name_of_customer = values[16];
                            data.Hosting_organization = values[17];
                            data.Set_data = values[18];
                            data.Update_data = values[19];
                            data.Purchase_stage = values[20];
                            data.Features_of_placing = values[21];
                            data.Start_data = values[22];
                            data.End_data = values[23];



                            if (values[24] == "" || values[24] == null)
                            {
                                data.Date_of_electronic_auction = "Нет данных";

                            }
                            else
                            {
                                data.Date_of_electronic_auction = values[24];
                            }
                        }
                        catch (Exception)
                        {
                            data.Purchase_object = "Ошибка чтения строки";
                            data.NMCK = 0;
                            data.Name_of_customer = "Ошибка чтения строки";
                            data.Hosting_organization = "Ошибка чтения строки";
                            data.Set_data = "Ошибка чтения строки";
                            data.Update_data = "Ошибка чтения строки";
                            data.Purchase_stage = "Ошибка чтения строки";
                            data.Features_of_placing = "Ошибка чтения строки";
                            data.Start_data = "Ошибка чтения строки";
                            data.End_data = "Ошибка чтения строки";



                            if (values[24] == "" || values[24] == null)
                            {
                                data.Date_of_electronic_auction = "Нет данных";

                            }
                            else
                            {
                                data.Date_of_electronic_auction = "Ошибка чтения строки";
                            }
                        }
                       

                        // Продолжайте аналогично для других свойств вашей модели

                        await _purchRepository.Create(data);
                       
                    }
                    return new BaseResponse<Purch>()
                    {
                        StatusCode = StatusCode.OK,
                        Data = data
                    };

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Purch>()
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
                var purch = await _purchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Purchase not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _purchRepository.Delete(purch);

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

        public async Task<IBaseResponse<Purch>> Edit(long id, PurchViewModel model)
        {
            try
            {
                var purch = await _purchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<Purch>()
                    {
                        Description = "Purchasing Data not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                purch.Federal_law = model.Federal_law;
                purch.Id = model.Id;
                purch.Method_of_purchasing = model.Method_of_purchasing;
                purch.Purchase_object = model.Purchase_object;
                purch.NMCK = Convert.ToDouble(model.NMCK);
                purch.Name_of_customer = model.Name_of_customer;
                purch.Hosting_organization = model.Hosting_organization;
                purch.Set_data = model.Set_data;
                purch.Update_data = model.Update_data;
                purch.Purchase_stage = model.Purchase_stage;
                purch.Features_of_placing = model.Features_of_placing;
                purch.Start_data = model.Start_data;
                purch.End_data = model.End_data;
                purch.Date_of_electronic_auction = purch.Date_of_electronic_auction;

                await _purchRepository.Update(purch);


                return new BaseResponse<Purch>()
                {
                    Data = purch,
                    StatusCode = StatusCode.OK,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Purch>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public  IBaseResponse<List<Purch>> GetPurchasingDatas()
            {
                try
                {

                    var purch = _purchRepository.GetAll().ToList();
                    if (!purch.Any())
                    {
                        return new BaseResponse<List<Purch>>()
                        {
                            Description = "Найдено 0 элементов",
                            StatusCode = StatusCode.OK
                        };
                    }

                    return new BaseResponse<List<Purch>>()
                    {
                        Data = purch,
                        StatusCode = StatusCode.OK
                    };
                }
                catch (Exception ex)
                {
                    return new BaseResponse<List<Purch>>()
                    {
                        Description = $"[GetPurchasingDatas] : {ex.Message}",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

            }

        public async Task<IBaseResponse<PurchViewModel>> GetPurchData(long id)
        {
            try
            {
                var purch = await _purchRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (purch == null)
                {
                    return new BaseResponse<PurchViewModel>()
                    {
                        Description = "Закупка не найдена",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PurchViewModel()
                {
                    Federal_law = purch.Federal_law,
                    Id = purch.Id,
                    Method_of_purchasing = purch.Method_of_purchasing,
                    Purchase_object = purch.Purchase_object,
                    NMCK = Convert.ToDouble(purch.NMCK),
                    Name_of_customer = purch.Name_of_customer,
                    Hosting_organization = purch.Hosting_organization,
                    Set_data = purch.Set_data,
                    Update_data = purch.Update_data,
                    Purchase_stage = purch.Purchase_stage,
                    Features_of_placing = purch.Features_of_placing,
                    Start_data = purch.Start_data,
                    End_data = purch.End_data,
                    Date_of_electronic_auction = purch.Date_of_electronic_auction,


                };

                return new BaseResponse<PurchViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PurchViewModel>()
                {
                    Description = $"[GetPurchData] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetPurchData(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var purch = await _purchRepository.GetAll()
                    .Select(x => new PurchViewModel()
                    {
                        Federal_law = x.Federal_law,
                        Id = x.Id,
                        Method_of_purchasing = x.Method_of_purchasing,
                        Purchase_object = x.Purchase_object,
                        NMCK = Convert.ToDouble(x.NMCK),
                        Name_of_customer = x.Name_of_customer,
                        Hosting_organization = x.Hosting_organization,
                        Set_data = x.Set_data,
                        Update_data = x.Update_data,
                        Purchase_stage = x.Purchase_stage,
                        Features_of_placing = x.Features_of_placing,
                        Start_data = x.Start_data,
                        End_data = x.End_data,
                        Date_of_electronic_auction = x.Date_of_electronic_auction,
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
    }
    }

