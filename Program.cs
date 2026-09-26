using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PracticeAuth.Data;
using PracticeAuth.Interfaces;
using PracticeAuth.Services;

namespace PracticeAuth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
                
        builder.Services.AddDbContext<ApplicationDbContext>((options) => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("myConnectionString")));

        // Add services to the container.
        builder.Services.AddScoped<IAuth,  AuthService>();
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            // Add this
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "API v1");
            });
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}