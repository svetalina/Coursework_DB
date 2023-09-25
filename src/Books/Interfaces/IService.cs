namespace Books.Interfaces
{
    public interface IService<T>
    {
        void Add(T model);
        void Update(T model);
        void Delete(T model);

        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}