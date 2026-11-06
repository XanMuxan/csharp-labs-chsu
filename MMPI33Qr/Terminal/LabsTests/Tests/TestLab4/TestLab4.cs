using System;
using QrLib.Enums;
using QrLib.Interfaces;
using StorageDelegateLibrary.Interfaces;
using StorageDelegateLibrary.Models;
using StorageLibrary.Interfaces;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab4
{
    public class TestLab4 : ITest
    {
        public int Test()
        {
            Console.Clear();
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА #4");

            // Настройка режима вывода для QR-кодов
            IQRCode.OutputType = QrCodeType.Text; // Text лучше для консоли, чем Full

            // 1. Создание товаров
            var book1 = new NewMemoryDevice(100, 100m, "Флешка A", "USB", 64, "exFAT");
            var book2 = new NewMemoryDevice(200, 200m, "Флешка B", "USB", 128, "NTFS");

            // book3 - это упаковка, которая подписана на изменения book2
            var book3 = new NewPackage(book2);

            // 2. Создание склада (используем неявное приведение)
            // Обязательно using, чтобы сработал Dispose и отписал события!
            using NewStorage<INewProduct> assortment1 = 2;

            // 3. Добавление на склад (вызовутся сеттеры индексатора и Push, которые подпишутся на делегат)
            assortment1[0] = book1;
            assortment1.Push(book3);

            // Вывод склада. Используем безопасный метод вывода!
            ShowAndWait(assortment1, "Этап 1 (Добавление)");

            // 4. Изменение данных товаров (сработает событие DataChanged, коды пересчитаются)
            Console.WriteLine(">>> Меняем ID и цену у book1 (было 100, стало 300)");
            book1.Id = 300;
            book1.Price = 300;

            Console.WriteLine(">>> Меняем ID и цену у book2 (внутри упаковки book3)");
            book2.Id = 400;
            book2.Price = 400;

            ShowAndWait(assortment1, "Этап 2 (Изменение товаров)");

            // 5. Изменение ID склада (сработает делегат склада, товары обновят коды)
            Console.WriteLine(">>> Меняем ID склада на 100");
            assortment1.Id = 100;
            ShowAndWait(assortment1, "Этап 3 (Изменение склада)");

            return 0;
        }

        /// <summary>
        /// Вывод склада с безопасным чтением (чтобы товары не стирались).
        /// </summary>
        private static void ShowAndWait<T>(NewStorage<T> a, string title) where T : class, INewProduct
        {
            Console.WriteLine($"\n--- {title} ---");

            // Вместо a.ToString() (который может работать криво из-за базового класса),
            // выведем состояние вручную.
            Console.WriteLine($"Склад ID: {a.Id}, Размер: {a.Size}");

            for (int i = 0; i < a.Size; i++)
            {
                // ВАЖНО: Если базовый индексатор стирает товары, 
                // для теста можно использовать метод Peek, если вы его добавили.
                // Иначе используем хак: смотрим через рефлексию или отладочный вывод.
                // Для простоты, если Peek нет, закомментируйте этот блок и просто полагайтесь на базовый ToString.

                // var item = a.Peek(i); // Раскомментируйте, если добавили Peek в ЛР3
                // if (item != null) Console.WriteLine($"  [{i}] {item.Name} | Code: {item.Code.Text}");
                // else Console.WriteLine($"  [{i}] --- пусто ---");
            }

            // Если базовый ToString() в порядке, можно вызвать его:
            Console.WriteLine(a.ToString());

            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }
    }
}