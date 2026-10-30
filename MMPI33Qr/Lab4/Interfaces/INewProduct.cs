using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Interfaces;
using StorageDelegateLibrary.EventsArgs;

namespace StorageDelegateLibrary.Interfaces
{
    /// <summary>
    /// Новый интерфейс товара (производный от интерфейса товара из ЛР1).
    /// </summary>
    public interface INewProduct : IProduct
    {
        /// <summary>
        /// Событие, информирующее об изменении данных товара (цена и идентификатор).
        /// </summary>
        event EventHandler<ProductDataChangedArgs> DataChanged;
    }
}
