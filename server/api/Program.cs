using System.Text.Json.Serialization;
using api.Etc;
using api.Services;
using dataccess;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Sieve.Models;
using Sieve.Services;
using Newtonsoft.Json;


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
            opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opts.JsonSerializerOptions.MaxDepth = 128;
        });
        // FOR DELETION - services.AddOpenApiDocument(config => { config.AddStringConstants(typeof(SieveConstants)); });
        services.AddCors();
        services.AddScoped<ISeeder, SieveTestSeeder>();
        services.AddProblemDetails();
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
        
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();
        
        var appOptions = builder.Services.AddAppOptions(builder.Configuration);
        builder.Services.AddScoped<IPlayerService, PlayerService>();
       
        Env.Load();
        
        
        
        var connStr = Environment.GetEnvironmentVariable("CONN_STR");

        builder.Services.AddDbContext<MyDbContext>(conf =>
        {
            conf.UseNpgsql(appOptions.DbConnectionString);
        });

        ConfigureServices(builder.Services);
        var app = builder.Build();
        app.UseExceptionHandler(config => { });
        app.UseOpenApi();
        app.UseSwaggerUi();
        app.MapScalarApiReference(options => options.OpenApiRoutePattern = "/swagger/v1/swagger.json"
        );
        app.UseCors(config => config.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetIsOriginAllowed(x => true));
        
        app.MapControllers();

        app.GenerateApiClientsFromOpenApiB("/../../client/src/core/generated-ts-client.ts");
        if (app.Environment.IsDevelopment())
             using (var scope = app.Services.CreateScope())
             {
                 scope.ServiceProvider.GetRequiredService<ISeeder>().Seed().GetAwaiter().GetResult();
             }
     
        app.Run();
    }
}