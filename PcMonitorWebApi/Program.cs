using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
}


