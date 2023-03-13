using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
  {
    var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(DbContextOptions<TryitterContext>));
    if (descriptor != null)
      services.Remove(descriptor);
    services.AddDbContext<TryitterContext>(options =>
        {
          options.UseInMemoryDatabase("InMemoryDbForTesting");
        });
    var sp = services.BuildServiceProvider();
    using (var scope = sp.CreateScope())
    using (var appContext = scope.ServiceProvider.GetRequiredService<TryitterContext>())
    {
      try
      {
        appContext.Database.EnsureCreated();

      }
      catch (Exception)
      {
        throw;
      }
    }
  });
  }
}