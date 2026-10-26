using Lab2.Interfaces;

namespace StorageLibrary.Interfaces
{
    /// <summary>
    /// Интерфейс склада
    /// </summary>
    public interface IStorage<T> where T : IProduct
    {
        /// <summary>
        /// Id склада
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Размер склада
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Индексатор для товара
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index] { get; set; }

        /// <summary>
        /// Добавить объект в пустую ячейку склада
        /// </summary>
        /// <param name="item">Помещаемый в пустую ячейку объект</param>
        public void Push(T item);
        public void Insert(int index, T item);
        public T Pop(int index);
        public void Swap(int index1, int index2);
        public void Replace(int index, T item);
        public int Find(int itemId);
        public int FindByName(string name);
        public void SortById();
        public void SortByName();
    }
}
