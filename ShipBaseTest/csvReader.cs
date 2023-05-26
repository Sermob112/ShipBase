using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBaseTest
{
    private List<Purch> GetPurchDataFromDatabase()
    {
        // Здесь вам нужно добавить код для получения данных из базы данных
        // и преобразования их в список объектов Purch

        List<Purch> data = new List<Purch>();

        // Пример кода для получения данных из базы данных
        using (var dbContext = new YourDbContext())
        {
            data = dbContext.Purchs.ToList();
        }

        return data;
    }
}
