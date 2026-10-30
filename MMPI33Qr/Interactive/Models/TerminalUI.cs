using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Enums;
using QrLib.Interfaces;
using StorageDelegateLibrary.Interfaces;
using StorageDelegateLibrary.Models;

namespace Interactive.Models
{
    /// <summary>
    /// Интерактивный терминал для работы с витринами и товарами.
    /// </summary>
    public class TerminalUI
    {
        // Список всех созданных витрин (складов)
        private readonly List<NewStorage<INewProduct>> _storages = new();
        // Список всех созданных товаров (для добавления на витрины)
        private readonly List<INewProduct> _availableProducts = new();

        private int _storageCounter = 1;

        public void Run()
        {
            IQRCode.OutputType = QrCodeType.Full;

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== ТЕРМИНАЛ УПРАВЛЕНИЯ АССОРТИМЕНТОМ ===");
                Console.WriteLine("1. Создать новую витрину (склад)");
                Console.WriteLine("2. Создать новый товар");
                Console.WriteLine("3. Показать все витрины");
                Console.WriteLine("4. Показать все доступные товары");
                Console.WriteLine("5. Сортировать товары на витрине");
                Console.WriteLine("6. Найти товар на витрине");
                Console.WriteLine("7. Удалить товар с витрины");
                Console.WriteLine("8. Очистить витрину полностью");
                Console.WriteLine("9. Изменить ID витрины");
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите действие: ");

                string input = Console.ReadLine();

                // Используем switch для выбора действия
                switch (input)
                {
                    case "1": CreateStorage(); break;
                    case "2": CreateProduct(); break;
                    case "3": ShowStorages(); break;
                    case "4": ShowProducts(); break;
                    case "5": SortProducts(); break;
                    case "6": FindProduct(); break;
                    case "7": RemoveProduct(); break;
                    case "8": ClearStorage(); break;
                    case "9": ChangeStorageId(); break;
                    case "0": exit = true; break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void CreateStorage()
        {
            Console.Clear();
            Console.WriteLine("--- СОЗДАНИЕ ВИТРИНЫ ---");
            Console.Write("Введите размер витрины: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0) return;

            Console.Write("Введите ID витрины (или 0 для авто): ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            if (id == 0) id = _storageCounter++;

            // Используем неявное приведение кортежа из ЛР4
            NewStorage<INewProduct> storage = (size, id);
            _storages.Add(storage);
            Console.WriteLine($"Витрина #{id} создана. Размер: {size}. Нажмите Enter...");
            Console.ReadLine();
        }

        private void CreateProduct()
        {
            Console.Clear();
            Console.WriteLine("--- СОЗДАНИЕ ТОВАРА ---");
            Console.Write("Введите ID товара: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Введите цену: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Введите наименование: ");
            string name = Console.ReadLine() ?? "Без имени";

            Console.Write("Создать упаковку? (1 - да, 0 - нет): ");
            bool isPackage = Console.ReadLine() == "1";

            INewProduct product;
            if (isPackage)
            {
                var inner = new NewMemoryDevice(id, price, name, "USB", 64, "exFAT");
                product = new NewPackage(inner);
            }
            else
            {
                product = new NewMemoryDevice(id, price, name, "USB", 64, "exFAT");
            }

            _availableProducts.Add(product);
            Console.WriteLine($"Товар '{name}' (ID: {product.Id}) создан. Нажмите Enter...");
            Console.ReadLine();
        }

        private void ShowStorages()
        {
            Console.Clear();
            if (_storages.Count == 0)
            {
                Console.WriteLine("Витрин пока нет. Нажмите Enter...");
                Console.ReadLine();
                return;
            }

            foreach (var storage in _storages)
            {
                // Выводит ID витрины, размер и QR-коды товаров
                Console.WriteLine(storage.ToString());
                Console.WriteLine(new string('-', 40));
            }
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        private void ShowProducts()
        {
            Console.Clear();
            if (_availableProducts.Count == 0)
            {
                Console.WriteLine("Нет доступных товаров. Нажмите Enter...");
                Console.ReadLine();
                return;
            }

            foreach (var p in _availableProducts)
            {
                Console.WriteLine($"[ID: {p.Id}] {p.Name} | Цена: {p.Price} | Тип: {p.Type}");
                Console.WriteLine($"Описание: {p.Description}");
                Console.WriteLine($"Доп. инфо: {p.AdditionalInformation}");
                Console.WriteLine($"Код: {p.Code.Text}");
                Console.WriteLine(new string('-', 20));
            }
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        private void SortProducts()
        {
            Console.Clear();
            var storage = SelectStorage();
            if (storage == null) return;

            Console.WriteLine("1. Сортировать по ID");
            Console.WriteLine("2. Сортировать по Наименованию");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": storage.SortById(); break;
                case "2": storage.SortByName(); break;
                default: return;
            }

            Console.WriteLine("Отсортировано! Нажмите Enter...");
            Console.ReadLine();
        }

        private void FindProduct()
        {
            Console.Clear();
            var storage = SelectStorage();
            if (storage == null) return;

            Console.WriteLine("1. Искать по ID");
            Console.WriteLine("2. Искать по Наименованию");
            string choice = Console.ReadLine();

            int position = -1;
            if (choice == "1")
            {
                Console.Write("Введите ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                    position = storage.Find(id);
            }
            else if (choice == "2")
            {
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                position = storage.FindByName(name);
            }

            if (position != -1)
                Console.WriteLine($"Товар найден на позиции {position}.");
            else
                Console.WriteLine("Товар не найден.");

            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        private void RemoveProduct()
        {
            Console.Clear();
            var storage = SelectStorage();
            if (storage == null) return;

            Console.Write("Введите позицию для извлечения: ");
            if (int.TryParse(Console.ReadLine(), out int pos))
            {
                // Используем индексатор (он очищает ячейку)
                var product = storage[pos];
                if (product != null)
                {
                    _availableProducts.Add(product); // возвращаем в список доступных
                    Console.WriteLine($"Товар '{product.Name}' извлечен со склада.");
                }
                else
                {
                    Console.WriteLine("Эта ячейка пуста.");
                }
            }
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        private void ClearStorage()
        {
            Console.Clear();
            var storage = SelectStorage();
            if (storage == null) return;

            Console.WriteLine("Очистка витрины...");

            // Чтобы очистить, мы забираем все товары через индексатор
            for (int i = 0; i < storage.Size; i++)
            {
                var item = storage[i];
                if (item != null) _availableProducts.Add(item);
            }
            Console.WriteLine("Витрина очищена. Товары возвращены в хранилище.");
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        private void ChangeStorageId()
        {
            Console.Clear();
            var storage = SelectStorage();
            if (storage == null) return;

            Console.Write("Введите новый ID витрины: ");
            if (int.TryParse(Console.ReadLine(), out int newId))
            {
                storage.Id = newId;
                Console.WriteLine($"ID витрины изменен на {newId}. Коды товаров обновлены.");
            }
            Console.WriteLine("Нажмите Enter...");
            Console.ReadLine();
        }

        // Вспомогательный метод для выбора склада
        private NewStorage<INewProduct> SelectStorage()
        {
            if (_storages.Count == 0)
            {
                Console.WriteLine("Нет созданных витрин. Нажмите Enter...");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("Доступные витрины:");
            for (int i = 0; i < _storages.Count; i++)
            {
                Console.WriteLine($"{i}. Витрина ID: {_storages[i].Id} (размер: {_storages[i].Size})");
            }

            Console.Write("Выберите номер витрины: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < _storages.Count)
                return _storages[idx];

            Console.WriteLine("Неверный выбор.");
            Console.ReadLine();
            return null;
        }
    }
}
