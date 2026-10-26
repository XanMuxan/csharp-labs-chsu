using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab2.Interfaces;
using QrLib.Interfaces;
using QrLib.Models;

namespace Lab2.Models
{
    /// <summary>
    /// Модель упаковки товара
    /// </summary>
    public class Package : Product
    {
        private readonly IProduct _product;

        public Package(IProduct product)
            : base(product.Id, product.Price * 1.10m, new PackageCode())
        {
            _product = product;
        }

        /// <summary>
        /// Возвращает тип упакованного товара.
        /// </summary>
        public override string Type => $"Упаковка ({_product.Type})";

        /// <summary>
        /// Возвращает информацию об упакованном товаре.
        /// </summary>
        public override string Description => _product.Description;
    }
}
