using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Enums;

namespace QrLib.Data
{
    public record QrCodeData
    {
        public QR Version { get; init; }
        public EccLevel CorrectionLevel { get; init; }
        public string Data { get; init; }
    }
}
