using System;
using Lab2.Interfaces;
using Lab2.Models;
using QrLib.Enums;
using QrLib.Interfaces;
using StorageLibrary.Models;
using Terminal.LabsTests.Interfaces;

namespace Terminal.LabsTests.Tests.TestLab3;

/// <summary>
/// Тестирование лабораторной работы №3.
/// </summary>
internal class TestLab3 : ITest
{
    /// <summary>
    /// Автоматический тест
    /// </summary>
    public int Test()
    {
        Console.Clear();
        Console.WriteLine("=== АВТОМАТИЧЕСКИЙ ТЕСТ ===");

        IQRCode.OutputType = QrCodeType.Full;

        IProduct device1 = new MemoryDevice(100, 100m, "Флешка A", "USB", 64, "exFAT");
        var device2 = new Package(new MemoryDevice(200, 200m, "Флешка B", "USB", 128, "NTFS"));

        Storage<IProduct> assortment1 = 10;
        Storage<Package> assortment2 = 10;

        assortment1[0] = device1;
        assortment1.Push(device2);
        assortment1.SortById();
        ShowAndWait(assortment1, "Этап 1");

        assortment1.Id = 100;
        ShowAndWait(assortment1, "Этап 2");

        var package = assortment1[assortment1.Find(device2.Id)];
        assortment2[0] = (Package)package;

        ShowAndWait(assortment1, "Этап 3");
        ShowAndWait(assortment2, "Этап 4");

        // Запускаем интерактивное меню после автотеста
        RunInteractiveMenu();

        return 0;
    }

    private void ShowAndWait<T>(Storage<T> a, string title) where T : IProduct
    {
        Console.WriteLine($"--- {title} ---");
        Console.WriteLine(a.ToString());
        Console.WriteLine("Нажмите Enter...");
        Console.ReadLine();
    }

    // ====================================================================
    // ИНТЕРАКТИВНОЕ МЕНЮ
    // ====================================================================
    private void RunInteractiveMenu()
    {
        Console.Clear();
        Console.WriteLine("=== ИНТЕРФЕЙС СКЛАДА ===");

        // Создаем склад на 5 ячеек для тестов
        Storage<IProduct> storage = 5;
        Console.WriteLine($"Создан склад #{storage.Id} размером {storage.Size} ячеек.\n");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Показать содержимое склада");
            Console.WriteLine("2. Добавить товар (в первую пустую ячейку)");
            Console.WriteLine("3. Добавить товар (в конкретную ячейку)");
            Console.WriteLine("4. Извлечь товар (по индексу)");
            Console.WriteLine("5. Заменить товар (по индексу)");
            Console.WriteLine("6. Поменять товары местами");
            Console.WriteLine("7. Найти товар по ID");
            Console.WriteLine("8. Найти товар по наименованию");
            Console.WriteLine("9. Сортировать по ID");
            Console.WriteLine("10. Сортировать по наименованию");
            Console.WriteLine("11. Изменить ID склада");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(storage);
                    break;

                case "2":
                    Console.Clear();
                    var newProduct1 = CreateMockProduct();
                    storage.Push(newProduct1);
                    Console.WriteLine("Товар добавлен в первую свободную ячейку.");
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("Введите индекс ячейки (0-4): ");
                    if (int.TryParse(Console.ReadLine(), out int idxInsert) && idxInsert >= 0 && idxInsert < storage.Size)
                    {
                        var newProduct2 = CreateMockProduct();
                        storage.Insert(idxInsert, newProduct2);
                        Console.WriteLine($"Товар добавлен в ячейку {idxInsert}.");
                    }
                    else Console.WriteLine("Неверный индекс.");
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("Введите индекс для извлечения: ");
                    if (int.TryParse(Console.ReadLine(), out int idxPop))
                    {
                        var popped = storage.Pop(idxPop);
                        Console.WriteLine(popped != null ? $"Извлечен товар:\n{popped}" : "Ячейка пуста или индекс неверен.");
                    }
                    break;

                case "5":
                    Console.Clear();
                    Console.Write("Введите индекс для замены: ");
                    if (int.TryParse(Console.ReadLine(), out int idxReplace))
                    {
                        var replaceProduct = CreateMockProduct();
                        storage.Replace(idxReplace, replaceProduct);
                        Console.WriteLine($"Товар в ячейке {idxReplace} заменен.");
                    }
                    break;

                case "6":
                    Console.Clear();
                    Console.Write("Введите первый индекс: ");
                    int idx1 = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Введите второй индекс: ");
                    int idx2 = int.Parse(Console.ReadLine() ?? "0");
                    storage.Swap(idx1, idx2);
                    Console.WriteLine("Товары поменялись местами (если индексы верны).");
                    break;

                case "7":
                    Console.Clear();
                    Console.Write("Введите ID товара для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out int searchId))
                    {
                        int pos = storage.Find(searchId);
                        Console.WriteLine(pos != -1 ? $"Товар найден на позиции {pos}." : "Товар не найден.");
                    }
                    break;

                case "8":
                    Console.Clear();
                    Console.Write("Введите наименование товара: ");
                    string searchName = Console.ReadLine();
                    int posName = storage.FindByName(searchName);
                    Console.WriteLine(posName != -1 ? $"Товар найден на позиции {posName}." : "Товар не найден.");
                    break;

                case "9":
                    Console.Clear();
                    storage.SortById();
                    Console.WriteLine("Склад отсортирован по ID. Текущее состояние:");
                    Console.WriteLine(storage);
                    break;

                case "10":
                    Console.Clear();
                    storage.SortByName();
                    Console.WriteLine("Склад отсортирован по наименованию. Текущее состояние:");
                    Console.WriteLine(storage);
                    break;

                case "11":
                    Console.Clear();
                    Console.Write("Введите новый ID склада: ");
                    if (int.TryParse(Console.ReadLine(), out int newId))
                    {
                        storage.Id = newId;
                        Console.WriteLine($"ID склада изменен на {newId}. Коды товаров обновлены.");
                    }
                    break;

                case "0":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        
            if (!exit)
            {
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    /// <summary>
    /// Вспомогательный метод для генерации случайного товара через меню.
    /// </summary>
    private IProduct CreateMockProduct()
    {
        Console.WriteLine("Введите id товара: "); int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите price товара:"); decimal price = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("Введите name товара:"); string name = Console.ReadLine() ?? "NoName";
        Console.WriteLine("Введите type товара:"); string type = Console.ReadLine() ?? "NoType";
        Console.WriteLine("Введите capacity товара:"); double capacity = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите memoryType товара:"); string memoryType = Console.ReadLine() ?? "NoMemoryType";

        IProduct memoryDevice = new MemoryDevice(id, price, name, type, capacity, memoryType);
        IProduct package = new Package(memoryDevice);
        return package;
    }
}
