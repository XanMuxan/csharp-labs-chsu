using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Interfaces;
using StorageLibrary.Extensions;
using StorageLibrary.Interfaces;

namespace StorageLibrary.Models
{
    /// <summary>
    /// Обобщённый класс-контейнер склада.
    /// </summary>
    /// <typeparam name="T">Тип товара, реализующий IProduct.</typeparam>
    public class Storage<T> : IStorage<T> where T : IProduct
    {
        // === Атрибуты класса ===
        private static int _nextId = 1;
        private readonly T[] _items;
        private int _id;

        /// <summary>
        /// Защищённый конструктор
        /// </summary>
        /// <param name="size">Количество ячеек склада.</param>
        protected Storage(int size)
        {
            if (size < 0) size = 0;
            _items = new T[size];
            _id = _nextId++;
        }

        /// <summary>Неявное приведение int к складу</summary>
        public static implicit operator Storage<T>(int size) => new Storage<T>(size);

        /// <summary>Идентификатор склада. При смене обновляет коды всех товаров.</summary>
        public virtual int Id
        {
            get => _id;
            set
            {
                _id = value;
                for (int i = 0; i < _items.Length; i++)
                    if (_items[i] != null)
                        _items[i].SetProductCode(_id, i);
            }
        }

        /// <summary>Размер склада.</summary>
        public int Size => _items.Length;

        /// <summary>
        /// Виртуальный индексатор. Получение «снимает» товар из ячейки.
        /// </summary>
        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Length) return default;
                T item = _items[index];
                _items[index] = default;
                return item;
            }
            set
            {
                if (index < 0 || index >= _items.Length) return;
                _items[index] = value;
                if (value != null) value.SetProductCode(_id, index);
            }
        }

        /// <summary>Добавление в первую пустую ячейку.</summary>
        public void Push(T item)
        {
            if (item == null) return;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    item.SetProductCode(_id, i);
                    return;
                }
            }
        }

        /// <summary>Добавление в конкретную позицию.</summary>
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= _items.Length || item == null) return;
            _items[index] = item;
            item.SetProductCode(_id, index);
        }

        /// <summary>Доставание товара по индексу.</summary>
        public T Pop(int index) => this[index];

        /// <summary>Перестановка двух товаров. Коды обновляются.</summary>
        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= _items.Length ||
                index2 < 0 || index2 >= _items.Length) return;

            (_items[index1], _items[index2]) = (_items[index2], _items[index1]);

            if (_items[index1] != null) _items[index1].SetProductCode(_id, index1);
            if (_items[index2] != null) _items[index2].SetProductCode(_id, index2);
        }

        /// <summary>Замена товара по индексу.</summary>
        public void Replace(int index, T item) => this[index] = item;

        /// <summary>Поиск позиции по идентификатору товара.</summary>
        public virtual int Find(int productId)
        {
            for (int i = 0; i < _items.Length; i++)
                if (_items[i] != null && _items[i].Id == productId) return i;
            return -1;
        }

        /// <summary>Поиск позиции по наименованию товара.</summary>
        public virtual int FindByName(string name)
        {
            for (int i = 0; i < _items.Length; i++)
                if (_items[i] != null && _items[i].Name == name) return i;
            return -1;
        }

        /// <summary>Сортировка по идентификатору товара (виртуальная).</summary>
        public virtual void SortById()
        {
            for (int i = 0; i < _items.Length - 1; i++)
                for (int j = i + 1; j < _items.Length; j++)
                    if (CompareById(_items[i], _items[j]) > 0)
                        Swap(i, j);
        }

        /// <summary>Сортировка по наименованию товара (виртуальная).</summary>
        public virtual void SortByName()
        {
            for (int i = 0; i < _items.Length - 1; i++)
                for (int j = i + 1; j < _items.Length; j++)
                    if (CompareByName(_items[i], _items[j]) > 0)
                        Swap(i, j);
        }

        /// <summary>Строковое представление склада.</summary>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"Склад #{Id} (размер: {Size}):");
            for (int i = 0; i < _items.Length; i++)
            {
                string cell = _items[i] == null ? "— пусто —" : _items[i].ToString();
                sb.AppendLine($"[{i}] {cell}");
            }
            return sb.ToString();
        }

        private static int CompareById(T a, T b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return a.Id.CompareTo(b.Id);
        }

        private static int CompareByName(T a, T b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return 1;
            if (b == null) return -1;
            return string.Compare(a.Name, b.Name, StringComparison.Ordinal);
        }
    }
}
