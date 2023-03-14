using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordProcessor.ConsoleApp;
using WordProcessor.Domain.Repository;
using WordProcessor.Infrastructure;
using WordProcessor.Infrastructure.Repository;

try
{
    Host.CreateDefaultBuilder()
        .ConfigureServices(ConfigureServices)
        .Build()
        .Services
        .GetRequiredService<App>()
        .Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    services.AddSingleton<IDictionaryRepository, DictionaryRepository>();
    services.AddDbContext<WordProcessorContext>(option => option.UseSqlServer("MS SQLServerConnectionString"));
}