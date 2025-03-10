using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PcMonitorWebApi.Data;
using PcMonitorWebApi.Servises;
using PcMonitorWebApi.Servises.Interfaces;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // ��������� Serilog ��� �����������
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.WriteTo.Console();
            configuration.WriteTo.File(path: "logs/log-.txt",
                                       rollOnFileSizeLimit: true,
                                       fileSizeLimitBytes: 50 * 1024 * 1024,
                                       retainedFileCountLimit: null, // ������� ��� ������
                                       shared: true);
        });

        // ����������� ���� ������ (SQLite �� ���������)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


        // ���������� �������� � ���������
        builder.Services.AddControllers();

        //���������� ��������
        builder.Services.AddScoped<IComputerService, ComputerService>();

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

        // �������������� �������� ������ ��� �������
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate(); // ��������� �������� (��� ������� �������, ���� �� ���)
        }

        // ��������� HTTP ��������� ��������
        
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PC Monitor API V1");
            });
        

        app.UseHttpsRedirection();

        // ��������� ���������
        app.MapControllers();

        app.Run();
    }
}


