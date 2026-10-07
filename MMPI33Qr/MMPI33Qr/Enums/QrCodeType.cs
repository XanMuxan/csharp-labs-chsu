using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrLib.Enums
{

    /// <summary> 
    ///     Формат вывода QR-кода, должен быть в отдельно файле
    /// </summary> 
    public enum QrCodeType
    {
        /// <summary> 
        ///     Текстовая информация 
        /// </summary> 
        Text,
        /// <summary> 
        ///     QR-код 
        /// </summary> 
        QrCode, 
        /// <summary> 
        ///     Полная информация: QR-код + текст
        /// </summary> 
        Full
    }
}
