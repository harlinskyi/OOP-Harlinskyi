namespace Lab5v7.Repository;


public interface IRepository<T>
{
    void Add(T item);
    bool Remove(Predicate<T> match);
    IEnumerable<T> Where(Func<T, bool> predicate);
    T? FirstOrDefault(Func<T, bool> predicate);
    IReadOnlyList<T> All();
}