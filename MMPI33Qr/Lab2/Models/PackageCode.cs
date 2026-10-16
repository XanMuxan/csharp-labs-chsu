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
        private string _text = string.Empty;
        private string _code = string.Empty;
        private QR _version = QR.V1;
        private EccLevel _correctionLevel = EccLevel.M;
        private Mask? _mask = null;

        /// <summary>
        /// Исходный текст. При установке автоматически пересчитывает QR-код.
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value ?? string.Empty;
                Rebuild();
            }
        }

        /// <summary>
        /// Пересчитывает QR-код на основе текущего текста.
        /// </summary>
        private void Rebuild()
        {
            if (string.IsNullOrEmpty(_text))
            {
                _code = string.Empty;
                return;
            }

            var version = _version;
            var ecc = (EccLevel?)_correctionLevel;
            var mask = _mask;
            var mode = _text.ToEncodingMode();

            _code = _text.ToQrCode(ref version, ref mode, ref ecc, ref mask);

            _version = version;
            _correctionLevel = ecc ?? EccLevel.M;
            _mask = mask;
        }

        /// <summary>
        /// Получение информации по QR-коду.
        /// </summary>
        public string QrInfo => $"Version: {_version}, ECC: {_correctionLevel}, Mask: {_mask?.ToString() ?? "auto"}";

        /// <summary>
        /// Общий способ формирования вывода QR-кода.
        /// </summary>
        public string QrCode => _code;

        /// <summary>
        /// Перегрузка ToString с учётом способа вывода.
        /// </summary>
        public override string ToString()
        {
            return IQRCode.OutputType switch
            {
                QrCodeType.Text => $"\t* {_text} *",
                QrCodeType.QrCode => _code,
                QrCodeType.Full => $"{_code}\n\t* {_text} *",
                _ => $"{_code}\n\t* {_text} *"
            };
        }
    }
}
