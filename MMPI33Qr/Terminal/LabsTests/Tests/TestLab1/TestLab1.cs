using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Enums;
using QrLib.Interfaces;
using QrLib.Models;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab1
{
    public class TestLab1 : ITest
    {
        public int Test() 
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Выберите тип отображения(1. Текст; 2. Код; 3 и т.д. Код+Текст )");
            string inp = Console.ReadLine() ?? "3";
            switch (inp)
            {
                case "1":
                    {
                        IQRCode.OutputType = QrCodeType.Text;
                        break;
                    }
                case "2":
                    {
                        IQRCode.OutputType = QrCodeType.QrCode;
                        break;
                    }
                default:
                    {
                        IQRCode.OutputType = QrCodeType.Full;
                        break;
                    }
            }
            // 1. Создаём объект (конструктор не используется — только инициализатор)
            var qr = new QRCode();

            // 2. Задаём текст — код пересчитается автоматически
            Console.Write("Введите текст: ");
            string input = Console.ReadLine() ?? "Hello world!";
            for (int i = 0; i < 1000000l; i++)
                qr.Text = input;


            Console.WriteLine(qr);

            // 3. Информация по QR-коду
            Console.WriteLine("\n=== Info ===");
            Console.WriteLine(qr.QrInfo);

            return 0;
        }   
    }
}
