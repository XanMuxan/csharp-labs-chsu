using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageDelegateLibrary.EventsArgs
{
    /// <summary>
    /// Аргументы события изменения данных товара.
    /// Имя класса заканчивается на Args (по правилам наименования).
    /// </summary>
    public class ProductDataChangedArgs : EventArgs
    {
        /// <summary>Новый идентификатор товара.</summary>
        public int NewId { get; }

        /// <summary>Новая цена товара.</summary>
        public decimal NewPrice { get; }

        /// <summary>
        /// Основной конструктор с двумя параметрами.
        /// </summary>
        /// <param name="newId">Новый идентификатор.</param>
        /// <param name="newPrice">Новая цена.</param>
        public ProductDataChangedArgs(int newId, decimal newPrice)
        {
            NewId = newId;
            NewPrice = newPrice;
        }
    }
}
