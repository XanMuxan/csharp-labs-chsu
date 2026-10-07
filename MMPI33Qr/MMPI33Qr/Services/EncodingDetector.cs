using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Enums;

namespace QrLib.Services
{
    /// <summary>Определение режима кодирования по тексту.</summary>
    internal static class EncodingDetector
    {
        private const string AlphaNumericChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ $%*+-./:";

        /// <summary>Определяет подходящий режим кодирования.</summary>
        public static EncodingMode ToEncodingMode(this string text)
        {
            if (string.IsNullOrEmpty(text)) return EncodingMode.Binary;
            if (text.All(char.IsDigit)) return EncodingMode.Numeric;
            if (text.ToUpperInvariant().All(c => AlphaNumericChars.Contains(c)))
                return EncodingMode.AlphaNumeric;
            return EncodingMode.Binary;
        }
    }
}
