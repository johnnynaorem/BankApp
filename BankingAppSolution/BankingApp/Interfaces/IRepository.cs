namespace BankingApp.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> Get(K key);
        Task<IEnumerable<T>> GetAll();
        Task<T> Delete(K key);
        Task <T> Update(K key, T entity);
        Task<T> Create(T entity);

    }
}
