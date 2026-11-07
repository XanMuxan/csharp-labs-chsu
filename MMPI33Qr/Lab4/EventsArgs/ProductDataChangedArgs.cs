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
    /// <remarks>
    /// Основной конструктор с двумя параметрами.
    /// </remarks>
    /// <param name="newId">Новый идентификатор.</param>
    /// <param name="newPrice">Новая цена.</param>
    public class ProductDataChangedArgs(int newId, decimal newPrice) : EventArgs
    {
        /// <summary>Новый идентификатор товара.</summary>
        public int NewId { get; } = newId;

        /// <summary>Новая цена товара.</summary>
        public decimal NewPrice { get; } = newPrice;
    }
}
