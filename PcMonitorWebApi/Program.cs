using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // ���������� �������� � ���������
        builder.Services.AddControllers();

        // ���������� ��������� Swagger/OpenAPI
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

        // ��������� HTTP ��������� ��������
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PC Monitor API V1");
            });
        }

        app.UseHttpsRedirection();

        // ��������� ���������
        app.MapControllers();

        app.Run();
    }
}


