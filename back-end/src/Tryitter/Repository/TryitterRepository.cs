using Microsoft.EntityFrameworkCore;

public class TryitterRepository<T> : ITryitterRepository<T> where T : class {
    protected readonly TryitterContext _context;
    public TryitterRepository(TryitterContext context)
    {
        _context = context;
    }

    public virtual async Task Add<T>(T entity) where T : class
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update<T>(T entity) where T : class
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T?> GetById<T>(int id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAll<T>() where T : class
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<Post>> GetAllById(int id)
    {
        return await _context.Set<Post>().Include(p => p.User).Where(p => p.UserId == id).ToListAsync();
    }

    public virtual async Task<User?> GetByNameOrEmail(string value)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(s => s.Email == value || s.UserName == value);
    }

}