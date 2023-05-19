using Microsoft.AspNetCore.Http;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.DAL.SectionOne.Repositories;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Enum;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Purch;
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
        public async Task<IBaseResponse<Purch>> Create(IFormFile filePath)
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
        }
    }

