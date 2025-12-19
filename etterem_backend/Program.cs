
using etterem_backend.Models;
using etterem_backend.Models.Dtos;
using etterem_backend.Services;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.Json.Serialization;

namespace etterem_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<EtteremContext>();
            builder.Services.AddScoped<IRendeles, RendelesService>();
            builder.Services.AddScoped<ITermek, TermekService>();
            builder.Services.AddScoped<ResponseDto>();

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
