using QrLib.Enums;

namespace QrLib.Interfaces
{
    public interface IQRCode
    {
        /// <summary>
        /// Общий способ формирования вывода QR-кода.
        /// </summary>
        /// <returns>Строковое представление QR-кода.</returns>
        string QrCode { get; }

        /// <summary>
        /// Получение информации по QR-коду (версия, маска, УКО).
        /// </summary>
        /// <returns>Строка с информацией.</returns>
        string QrInfo { get; }

        /// <summary>
        /// Получение исходного текста.
        /// </summary>
        string Text { get; set; }

        // === Статическое поле: способ вывода для ВСЕХ объектов ===
        public static QrCodeType OutputType { get; set; } = QrCodeType.Full;
    }
}
