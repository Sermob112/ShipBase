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

                        data = new Purch()
                        {
                            Federal_law = values[0],
                            Id = long.Parse(values[1].Replace("№", "")),
                            Method_of_purchasing = values[2],
                            Purchase_object = values[3],
                            Purchase_stage = values[4]

                        };


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

