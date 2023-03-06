using Microsoft.EntityFrameworkCore;

public interface ITryitterRepository<T> where T : class
{

    Task<T?> GetById<T>(int id) where T : class;
    Task<IEnumerable<T>> GetAll<T>() where T : class;
    Task Add<T>(T entity) where T : class;
    Task Update<T>(T entity) where T : class;
    Task Delete<T>(T entity) where T : class;
    Task<Student>? GetByNameOrEmail(string value);
    Task<IEnumerable<Post>> GetAllById(int id);

}