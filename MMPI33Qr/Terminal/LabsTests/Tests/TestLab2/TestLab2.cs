using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Interfaces;
using Lab2.Models;
using QrLib.Enums;
using QrLib.Interfaces;
using QrLib.Models;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab2
{
    public class TestLab2 : ITest
    {
        public int Test()
        {
            Console.Clear();
            Console.WriteLine("Лабараторная работа 2");

            IQRCode.OutputType = QrCodeType.Full;

            Console.WriteLine("Введите id товара: "); int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите price товара:"); decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите name товара:"); string name = Console.ReadLine() ?? "NoName";
            Console.WriteLine("Введите type товара:"); string type = Console.ReadLine() ?? "NoType";
            Console.WriteLine("Введите capacity товара:"); double capacity = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите memoryType товара:"); string memoryType = Console.ReadLine() ?? "NoMemoryType";

            IProduct memoryDevice = new MemoryDevice(id, price, name, type, capacity, memoryType);
            IProduct package = new Package(memoryDevice);

            Console.WriteLine(memoryDevice);
            Console.WriteLine("Упакованный товар");
            Console.WriteLine(package);

            return 0;
        }
    }
}
