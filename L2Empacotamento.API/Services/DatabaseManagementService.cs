using L2Empacotamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace L2Empacotamento.API.Services;

public static class DatabaseManagementService
{
    //using (var serviceScope = app.ApplicationServices.CreateScope())
    //    {
    //        var dbContext = serviceScope.ServiceProvider.GetRequiredService<EmpacotamentoDbContext>();
    //        dbContext.Database.Migrate();
    //    }
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<EmpacotamentoDbContext>();

        try
        {
            var databaseCreator = dbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect())
                {
                    Console.WriteLine("Não conseguiu conectar. Criando o banco...");
                    databaseCreator.Create();
                }

                if (!databaseCreator.HasTables())
                {
                    Console.WriteLine("Banco vazio. Criando tabelas...");
                    databaseCreator.CreateTables();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inicializar banco: " + ex.Message);
        }
    }
}
