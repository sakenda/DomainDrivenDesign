using DomainDrivenDesign.Application.Kontoeroeffnung;
using DomainDrivenDesign.Application.Kontoführung;
using DomainDrivenDesign.Persistance.Database;
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
        builder.Services.AddDbContext<BankDbContext>(options =>
            options.UseInMemoryDatabase("BankDB"));

        builder.Services.AddScoped<Persistence.Kontoeroeffnung.KundenRepository>();
        builder.Services.AddScoped<Persistence.Kontoeroeffnung.GirokontoRepository>();
        builder.Services.AddScoped<KontoeroeffnungService>();

        builder.Services.AddScoped<Persistance.Kontofuehrung.KundeRepository>();
        builder.Services.AddScoped<Persistance.Kontofuehrung.GirokontoRepository>();
        builder.Services.AddScoped<KontofuehrungService>();


        var app = builder.Build();

        // Demo-Daten seeden
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();
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
