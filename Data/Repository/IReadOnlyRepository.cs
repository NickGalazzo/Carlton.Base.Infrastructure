namespace Carlton.Base.Infrastructure.Data.Repository;

public interface IReadOnlyRepository<T, TId>
{
    Task<T> FindById(TId id);
    Task<IEnumerable<T>> FindAll();
}

