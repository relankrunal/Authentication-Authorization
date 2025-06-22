using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using ServiceCore.Interfaces.Repositories;
using ServiceCore.Interfaces.Services;
using ServiceCore.Services;
using Infrastructure.Repositories;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AuthDb"));

builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
builder.Services.AddScoped<IJwtService>(_ => new JwtService("supersecretkey", "DemoIssuer"));
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
