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
    /// Новый класс товара (производный от MemoryDevice из ЛР2).
    /// </summary>
    public class NewMemoryDevice : MemoryDevice, INewProduct
    {
        public event EventHandler<ProductDataChangedArgs> DataChanged;

        public NewMemoryDevice(int id, decimal price, string name, string type, double capacity, string memoryType)
            : base(id, price, name, type, capacity, memoryType) { }

        /// <summary>Перегрузка ID с вызовом события.</summary>
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
    }
}
