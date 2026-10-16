using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QrLib.Interfaces;
using QrLib.Models;

namespace Lab2.Models
{
    /// <summary>
    /// Модель запоминающего устройства
    /// </summary>
    public class MemoryDevice : Product
    {
        // === Атрибуты класса ===
        private string _name;
        private string _description;
        private IQRCode _code;
        private string _type;
        private string _additionalInformation;

        public double Capacity { get; set; }
        public string MemoryType { get; set; } // exFAT, NTFS
        public string Name { get => _name; set => _name = value; }
        public IQRCode Code { get => _code; }
        public override string Type { get => _type; }
        public string AdditionalInformation { get => _additionalInformation; }
        public override string Description { get => _description; }

        public MemoryDevice(int id, decimal price, string name, string type, double capacity, string memoryType) : base(id, price) 
        {
            _code = new QRCode();
            Name = name;
            MemoryType = memoryType;
            Capacity = capacity;
            _type = type;
            _description = $"Capacity: {Capacity}\nMemory type: {MemoryType}";
        }
    }
}
