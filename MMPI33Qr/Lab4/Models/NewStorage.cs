using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Models;
using StorageDelegateLibrary.EventsArgs;
using StorageDelegateLibrary.Interfaces;
using StorageLibrary.Extensions;
using StorageLibrary.Interfaces;
using StorageLibrary.Models;

namespace StorageDelegateLibrary.Models
{

    /// <summary>
    /// Запакованный обобщенный класс склада (производный от Storage<T> из ЛР3).
    /// </summary>
    public class NewStorage<T> : Storage<T>, IDisposable where T : class, INewProduct
    {
        private Action<IStorage<T>> _idChangedDelegate;
        /// <summary>
        /// Единственный закрытый конструктор. Принимает кортеж (размер, ID).
        /// </summary>
        private NewStorage(int size, int id) : base(size)
        {
            Id = id;
        }

        public static implicit operator NewStorage<T>(int size) => new NewStorage<T>(size, 0);
        public static implicit operator NewStorage<T>((int size, int id) tuple) => new NewStorage<T>(tuple.size, tuple.id);

        /// <summary>
        /// Перегрузка свойства ID. Вызывает делегат только при изменении и если есть подписчики.
        /// </summary>
        public override int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    _idChangedDelegate?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Перегрузка индексатора
        /// </summary>
        public override T this[int index]
        {
            get {
                T oldItem = Peek(index);
                if (oldItem != null)
                {
                    oldItem.DataChanged -= OnProductDataChanged;
                    _idChangedDelegate -= oldItem.SetProductCode;
                }
                return base[index]; 
            }
            set
            {
                T oldItem = Peek(index);
                if (oldItem != null)
                {
                    oldItem.DataChanged -= OnProductDataChanged;
                    _idChangedDelegate -= value.SetProductCode;
                }
                base[index] = value;

                if (value != null)
                {
                    _idChangedDelegate += value.SetProductCode;
                    value.DataChanged += OnProductDataChanged;
                }       
            }
        }

        private void OnProductDataChanged(object sender, ProductDataChangedArgs e)
        {
            if (sender is T product)
            {
                int pos = Find(e.NewId);
                if (pos != -1)
                    StorageLibrary.Extensions.ProductExtensions.SetProductCode(product, this.Id, pos);
            }
        }

        // Универсальный поиск через делегат Predicate
        private int FindPosition(Predicate<T> match)
        {
            for (int i = 0; i < this.Size; i++)
            {
                var item = Peek(i);
                if (item != null && match(item)) return i;
            }
            return -1;
        }

        public override int Find(int productId) => FindPosition(item => item.Id == productId);
        public override int FindByName(string name) => FindPosition(item => item.Name == name);

        // Универсальная сортировка через делегат Comparison
        private void SortInternal(Comparison<T> comparison)
        {
            for (int i = 0; i < this.Size - 1; i++)
            {
                for (int j = i + 1; j < this.Size; j++)
                {
                    var item1 = Peek(i);
                    var item2 = Peek(j);

                    int result = 0;
                    if (item1 == null && item2 != null) result = 1;
                    else if (item1 != null && item2 == null) result = -1;
                    else if (item1 != null && item2 != null) result = comparison(item1, item2);

                    if (result > 0)
                    {
                        base.Swap(i, j);
                        if (item1 != null) StorageLibrary.Extensions.ProductExtensions.SetProductCode(item1, this.Id, j);
                        if (item2 != null) StorageLibrary.Extensions.ProductExtensions.SetProductCode(item2, this.Id, i);
                    }
                }
            }
        }

        public override void SortById() => SortInternal((a, b) => a.Id.CompareTo(b.Id));
        public override void SortByName() => SortInternal((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        /// <summary>
        /// Освобождение ресурсов и отписка от событий.
        /// </summary>
        public void Dispose()
        {
            _idChangedDelegate = null;
            for (int i = 0; i < this.Size; i++)
            {
                var item = this[i];
                if (item != null) item.DataChanged -= OnProductDataChanged;
            }
        }
    }
}
