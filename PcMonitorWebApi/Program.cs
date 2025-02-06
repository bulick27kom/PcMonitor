using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PcMonitorWebApi.Data;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Настройка Serilog для логирования
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console();
        });

        // Добавление DbContext с SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=db.db"));


        // Добавление сервисов в контейнер
        builder.Services.AddControllers();

        // Добавление поддержки Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PC Monitor API",
                Version = "v1"
            });
        });

        var app = builder.Build();

        // Инициализация базы данных
        InitializeDatabase(app);

        // Настройка HTTP конвейера запросов
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PC Monitor API V1");
            });
        }

        app.UseHttpsRedirection();

        // Настройка маршрутов
        app.MapControllers();

        app.Run();
    }

    private static void InitializeDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        // Применяем миграции автоматически при запуске приложения
        context.Database.Migrate();
    }
}


