using Microsoft.EntityFrameworkCore;

public class TryitterContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Post> Posts { get; set; } 

    public TryitterContext(DbContextOptions<TryitterContext> options) : base(options){ }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var server = Environment.GetEnvironmentVariable("DATABASE_SERVER")!;
        var dbName = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
        var user = Environment.GetEnvironmentVariable("DATABASE_USER")!;
        var pass = Environment.GetEnvironmentVariable("DATABASE_PASS")!;
        
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer($"Server={server};Database={dbName};User={user};Password={pass};");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasOne(p => p.Student)
            .WithMany(s => s.Posts).HasForeignKey(p => p.StudentId);
    }

}