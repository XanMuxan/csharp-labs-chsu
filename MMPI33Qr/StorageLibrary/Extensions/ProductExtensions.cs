using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Interfaces;
using StorageLibrary.Interfaces;

namespace StorageLibrary.Extensions
{
    /// <summary>
    /// Методы расширения для товара.
    /// </summary>
    public static class ProductExtensions
    {
        
        public static void SetProductCode(this IProduct product, int storageId, int position)
        {
            product.Code.Text = $"{storageId}|[{position}]:({product.Id}:{product.Price})";
        }

        public static void SetProductCode<T>(this IProduct product, IStorage<T> storage)
            where T : IProduct
        {
            int position = storage.Find(product.Id);
            product.SetProductCode(storage.Id, position);  // вызов перегрузки №1
        }
    }
}
