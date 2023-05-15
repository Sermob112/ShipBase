﻿using Microsoft.AspNetCore.Http;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Domain.SectionOne.Response;
using ShipBase.Domain.SectionOne.ViewModels.Purch;
using ShipBase.Domain.SectionOne.ViewModels.PurchasingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBase.Service.SectionOne.Interfaces
{
    public interface IPurchService
    {

        IBaseResponse<List<Purch>> GetPurchasingDatas();
 

        Task<IBaseResponse<Purch>> Create(IFormFile filePath);
    }
}
