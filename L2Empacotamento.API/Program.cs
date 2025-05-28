
using L2Empacotamento.Infrastructure.Persistence;
using L2Empacotamento.Infrastructure.Repositories;
using L2Empacotamento.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L2Empacotamento.API
{
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

            builder.Services.AddDbContext<EmpacotamentoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionL2")));

            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

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
