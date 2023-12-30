using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Project__nhom3.Data;
using Project__nhom3.Models;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AirlineDbContext");
builder.Services.AddDbContext<AirlineDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("MyCors", buid =>
{
    buid.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AirlineDbContext>();

    try
    {
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Kết nối cơ sở dữ liệu thành công!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi khi kết nối cơ sở dữ liệu: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseAuthentication(); // Bỏ qua authentication nếu bạn không dùng Identity
app.UseAuthorization();

app.MapControllers();

app.Run();
