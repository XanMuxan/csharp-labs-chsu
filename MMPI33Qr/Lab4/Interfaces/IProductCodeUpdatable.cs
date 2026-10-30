using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageDelegateLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с текстом Data Matrix/QR-кода.
    /// Необходим для делегирования обязанности обновления кода.
    /// </summary>
    public interface IProductCodeUpdatable
    {
        /// <summary>
        /// Текст Data Matrix/QR-кода.
        /// </summary>
        string CodeText { get; set; }
    }
}
