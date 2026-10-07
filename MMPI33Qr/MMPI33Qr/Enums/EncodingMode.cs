using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QrLib.Enums
{

    /// <summary> 
    /// QR Code encoding mode 
    /// </summary> 
    public enum EncodingMode : byte
    {
        /// <summary> 
        /// Numbers only 
        /// </summary> 
        Numeric,
        /// <summary> 
        /// Caps english + numbers 
        /// </summary> 
        AlphaNumeric,
        /// <summary> 
        /// Everything 
        /// </summary> 
        Binary,
        /// <summary> 
        /// だってばよ 
        /// </summary> 
        Kanji
    }
}
