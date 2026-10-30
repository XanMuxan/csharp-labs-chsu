using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Enums;
using QrLib.Interfaces;
using StorageDelegateLibrary.Interfaces;
using StorageDelegateLibrary.Models;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab4
{
    public class TestLab4 : ITest
    {
        public int Test()
        {
            Console.Clear();
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №4");

            // Настройка режима вывода для QR-кодов
            IQRCode.OutputType = QrCodeType.Full;

            // Создание товаров
            var book1 = new NewMemoryDevice(100, 100m, "Флешка A", "USB", 64, "exFAT");
            var book2 = new NewMemoryDevice(200, 200m, "Флешка B", "USB", 128, "NTFS");
            var book3 = new NewPackage(book2);

            // Создание склада через неявное приведение (размер = 2)
            using NewStorage<INewProduct> assortment1 = 2;

            // Добавление на склад
            assortment1[0] = book1;
            assortment1.Push(book3);
            ShowAndWait(assortment1, "Этап 1");

            // Изменение данных товаров (должны сработать события)
            book1.Id = 300;
            book1.Price = 300;
            book2.Id = 400;
            book2.Price = 400;
            ShowAndWait(assortment1, "Этап 2");

            // Изменение ID склада (должен сработать делегат склада)
            assortment1.Id = 100;
            ShowAndWait(assortment1, "Этап 3");
            return 0;
        }

        private static void ShowAndWait<T>(NewStorage<T> a, string title) where T : INewProduct
        {
            Console.WriteLine($"--- {title} ---");
            Console.WriteLine(a.ToString());
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }
    }
}
