using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.Extensions.Options;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<AppSettings>(builder.Configuration);
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

           // builder.Services.AddSingleton(AppSettins);
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<IClienteData, ClienteData>();
            builder.Services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            builder.Services.AddScoped<IContaCorrenteData, ContaCorrenteData>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


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
}