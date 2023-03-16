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
        .ConfigureServices(services => services.AddTransient<App>())
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
    services.AddTransient<IDictionaryRepository, DictionaryRepository>();
    services.AddDbContext<WordProcessorContext>(option => option.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WordProcessorDB;Trusted_Connection=True;"));//пытался добавить строку подключения в App.config но не вышло, пришлось на прямую прописать
}