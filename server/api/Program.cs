using System.Text.Json.Serialization;
using api.Etc;
using dataaccess;
using DotNetEnv;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Sieve.Models;
using Sieve.Services;
using DotNetEnv;


namespace api;
//test comment
public class Program
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.InjectAppOptions();
        services.AddControllers().AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            opts.JsonSerializerOptions.MaxDepth = 128;
        });
        services.AddOpenApiDocument(config => { config.AddStringConstants(typeof(SieveConstants)); });
        services.AddCors();
        services.AddScoped<ISeeder, SieveTestSeeder>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.Configure<SieveOptions>(options =>
        {
            options.CaseSensitive = false;
            options.DefaultPageSize = 10;
            options.MaxPageSize = 100;
        });
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
    }

    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
       
        Env.Load();
        
        var connStr = Environment.GetEnvironmentVariable("CONN_STR");

        builder.Services.AddDbContext<MyDbContext>(conf =>
        {
            conf.UseNpgsql(connStr);
        });

        ConfigureServices(builder.Services);
        var app = builder.Build();
        app.UseExceptionHandler(config => { });
        app.UseOpenApi();
        app.UseSwaggerUi();
        app.MapScalarApiReference(options => options.OpenApiRoutePattern = "/swagger/v1/swagger.json"
        );
        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(x => true));
        app.MapControllers();
        
        app.MapGet("/", ([FromServices]MyDbContext dbContext) =>
        {
            var myPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "biggus@dickus.com",
                Name = "Biggus"
            };
            dbContext.Add(myPlayer);
            dbContext.SaveChanges();
            var objects = dbContext.Players.ToList();
            return objects;
        });
        
        app.GenerateApiClientsFromOpenApi("/../../client/src/core/generated-client.ts").GetAwaiter().GetResult();
        if (app.Environment.IsDevelopment())
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ISeeder>().Seed().GetAwaiter().GetResult();
            }

        app.Run();
    }
}