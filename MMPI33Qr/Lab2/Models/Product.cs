using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Interfaces;
using QrLib.Models;
using QrLib.Interfaces;

namespace Lab2.Models
{
    /// <summary>
    /// Модель продукта
    /// </summary>
    public abstract class Product : IProduct
    {
        // === Атрибуты класса ===
        private int _id;
        private decimal _price;
        private readonly IQRCode _code;

        public virtual int Id { get { return _id; } set { _id = value; RebuildCode(); } }
        public virtual decimal Price { get { return _price; } set { _price = value; RebuildCode(); } }
        public string Name { get; set; }
        public IQRCode Code { get => _code; }
        public abstract string Type { get; }
        public string AdditionalInformation { get; }
        public abstract string Description { get; } 

        protected Product(int id, decimal price, IQRCode code) 
        {
            _code = code;
            _id = id;
            _price = price;
            RebuildCode();
        }

        public override string ToString()
        {
            return $"{Type}:{Name}\nId: {Id}\nPrice: {Price}\n{AdditionalInformation}\n{Description}\n{Code}";
        }

        /// <summary>
        /// Изменение кода продукта
        /// </summary>
        private void RebuildCode()
        {
            _code.Text = $"{Id}:{Price}";
        }
    }
}
