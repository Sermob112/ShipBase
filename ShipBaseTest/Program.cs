using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ShipBaseTest
{
    internal class Program
    {

       public class Purchase
        {
            public long Id { set; get; }
            public string Federal_law { set; get; }
            public string Method_of_purchasing { set; get; }
            public string Purchase_object { set; get; }
            public string Purchase_stage { set; get; }


        }



        static void Main(string[] args)
        {
            string t = "700000,000";
        
            double num = Convert.ToDouble(t);
            Console.WriteLine(num);

            /* string filePath = @"C:\\Users\\Sergey\\source\\repos\\ShipBase\\ShipBaseTest\\Test\\tester.csv";
             if (File.Exists(filePath)) {
                 List<Purchase> data = ReadDataFromCsv(filePath, Encoding.GetEncoding("Windows-1251"));
                     }*/



            /*Execl метод чтнение почти готовый*/
            /*ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string filePath2 = @"C:\\Users\\Sergey\\source\\repos\\ShipBase\\ShipBaseTest\\Test\\Work.xlsx";
            if (File.Exists(filePath2))
            {
                using (var package = new ExcelPackage(new FileInfo(filePath2)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Индексация начинается с 1, выберите правильный индекс

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    Purchase myObject = new Purchase();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            var cellValue = worksheet.Cells[row, col].Value;

                            // Присвоение значения соответствующему свойству объекта
                            if (col == 1)
                            {
                                myObject.Federal_law = cellValue?.ToString();
                            }
                            if (col == 2)
                            {
                                myObject.Id = long.Parse(cellValue.ToString().Replace("№", ""));
                            }
                            else if (col == 3)
                            {
                                myObject.Method_of_purchasing = cellValue.ToString();
                             

                            }
                            else if (col == 4)
                            {
                                myObject.Purchase_object = cellValue.ToString();


                            }
                            else if (col == 5)
                            {
                                myObject.Purchase_stage = cellValue.ToString();


                            }

                            // Продолжайте аналогично для других свойств вашего класса
                        }
                    }
                

                    // Дальнейшая обработка объекта myObject, содержащего данные из XLSX файла
                }
            }
            else
            {
                Console.WriteLine("Файл не существует.");
            }*/


            Console.ReadLine();
        }

        static List<Purchase> ReadDataFromCsv(string filePath, Encoding encoder)
        {
                List<Purchase> data = new List<Purchase>();

                using (StreamReader reader = new StreamReader(filePath, encoder))
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

              

                        Purchase obj = new Purchase();

                        obj.Federal_law = values[0];

                        if (long.TryParse(values[1].Replace("№",""), out long Id))
                        {
                            obj.Id = Id;
                        }
                        else
                        {
                            // Пропускаем строку, если возникла ошибка при преобразовании возраста
                            continue;
                        }
                        obj.Method_of_purchasing = values[2];
                        obj.Purchase_object = values[3];
                        obj.Purchase_stage = values[4];
                        // Продолжайте аналогично для других свойств вашей модели

                        data.Add(obj);
                    }
                }

                return data;
        }
    }

}

