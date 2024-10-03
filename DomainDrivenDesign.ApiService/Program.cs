using DomainDrivenDesign.Application.Kontoeroeffnung;
using DomainDrivenDesign.Persistence.Kontoeroeffnung;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Application;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Entity Framework Core with In-Memory Database
        builder.Services.AddDbContext<KontoeroeffnungDbContext>(options =>
            options.UseInMemoryDatabase("KontoeröffnungDB"));

        builder.Services.AddScoped<KundenRepository>();
        builder.Services.AddScoped<GirokontoRepository>();
        builder.Services.AddScoped<KontoeroeffnungService>();

        var app = builder.Build();

        // Demo-Daten seeden
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KontoeroeffnungDbContext>();
            context.Database.EnsureCreated(); // Dies sorgt dafür, dass die Datenbank erstellt wird und Seed-Daten angewendet werden
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
