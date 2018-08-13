using System.Linq;

namespace QAFood.DAL
{
    /// <summary>
    /// The respository interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        void Save();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        IQueryable<T> GetAll { get; }
        T Get(long id);
    }
}
