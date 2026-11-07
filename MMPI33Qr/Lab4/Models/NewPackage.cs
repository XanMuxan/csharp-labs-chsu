using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Models;
using StorageDelegateLibrary.EventsArgs;
using StorageDelegateLibrary.Interfaces;

namespace StorageDelegateLibrary.Models
{
    /// <summary>
    /// Новый класс упаковки
    /// </summary>
    public class NewPackage : Package, INewProduct, IDisposable
    {
        private readonly INewProduct _product;
        public event EventHandler<ProductDataChangedArgs> DataChanged;

        /// <summary>
        /// Единственный конструктор с параметром – интерфейсом на новый товар.
        /// </summary>
        public NewPackage(INewProduct product) : base(product)
        {
            _product = product;
            // Подписка на событие внутреннего товара
            _product.DataChanged += OnInnerProductChanged;
        }

        private void OnInnerProductChanged(object sender, ProductDataChangedArgs e)
        {
            // Обновляем свои данные при изменении товара
            base.Id = e.NewId;
            base.Price = e.NewPrice * 1.10m; // Наценка 10%

            // Информируем об изменении своих данных
            DataChanged?.Invoke(this, new ProductDataChangedArgs(this.Id, this.Price));
        }

        public override int Id
        {
            get => base.Id;
            set
            {
                if (base.Id != value)
                {
                    base.Id = value;
                    DataChanged?.Invoke(this, new ProductDataChangedArgs(value, base.Price));
                }
            }
        }

        /// <summary>Перегрузка Price с вызовом события.</summary>
        public override decimal Price
        {
            get => base.Price;
            set
            {
                if (base.Price != value)
                {
                    base.Price = value;
                    DataChanged?.Invoke(this, new ProductDataChangedArgs(base.Id, value));
                }
            }
        }

        /// <summary>
        /// Обязательная отписка от события при освобождении памяти.
        /// </summary>
        public void Dispose()
        {
            _product.DataChanged -= OnInnerProductChanged;
        }
    }
}
