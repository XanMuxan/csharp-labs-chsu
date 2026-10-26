using QrLib.Enums;
using QrLib.Interfaces;
using QrLib.Services;


namespace QrLib.Models
{
    /// <summary>
    /// Модель QR-кода. Хранит текст, версию, УКО, маску и готовый код.
    /// </summary>
    public class PackageCode : IQRCode
    {
        // === Атрибуты класса ===
        private IQRCode _qRCode = new QRCode();

        /// <summary>
        /// Исходный текст. При установке автоматически пересчитывает QR-код.
        /// </summary>
        public string Text { get => _qRCode.Text; set => _qRCode.Text = value; }

        /// <summary>
        /// Получение информации по QR-коду.
        /// </summary>
        public string QrInfo => _qRCode.QrInfo;

        /// <summary>
        /// Общий способ формирования вывода QR-кода.
        /// </summary>
        public string QrCode => _qRCode.QrCode;

        /// <summary>
        /// Перегрузка ToString с учётом способа вывода.
        /// </summary>
        public override string ToString()
        {
            return IQRCode.OutputType switch
            {
                QrCodeType.Text => $"\t* {Text} *",
                QrCodeType.QrCode => QrCode,
                QrCodeType.Full => $"{QrCode}\n\t* {Text} *",
                _ => $"{QrCode}\n\t* {Text} *"
            };
        }
    }
}
