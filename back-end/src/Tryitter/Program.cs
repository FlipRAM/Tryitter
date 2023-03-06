using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TryitterContext>();
builder.Services.AddScoped(typeof(ITryitterRepository<>), typeof(TryitterRepository<>));
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<PostService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() 
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!))
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditProfile", policy =>
        policy.RequireAssertion(context =>
        {
            var student = context.User.FindFirst("StudentId");
            if (student == null)
            {
                throw new ArgumentException("Student é nulo");
            }
            int.TryParse(student.Value, out int studentId);

            var httpContext = context.Resource as HttpContext;
            var routeData = httpContext!.GetRouteData();
            int.TryParse(routeData.Values["id"].ToString(), out int currentId);
            return studentId == currentId;
        }));   
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TryitterContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }