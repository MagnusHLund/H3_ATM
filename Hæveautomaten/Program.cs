using Hæveautomaten.Data;
using Hæveautomaten.Controllers;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Hæveautomaten
{
    public static class Program
    {
        public static void Main()
        {
            ServiceProvider serviceProvider = ConfigureServices();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IMainController myService = scope.ServiceProvider.GetRequiredService<IMainController>();
                myService.HandleMainMenuDisplay();
            }
        }

        private static ServiceProvider ConfigureServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IMainController, MainController>();
            serviceCollection.AddSingleton<IBankController, BankController>();
            serviceCollection.AddSingleton<IAdminController, AdminController>();
            serviceCollection.AddSingleton<IPersonController, PersonController>();
            serviceCollection.AddSingleton<IAccountController, AccountController>();
            serviceCollection.AddSingleton<IAutomatedTellerMachineController, AutomatedTellerMachineController>();

            // TODO: Add the actual connection string
            serviceCollection.AddDbContext<HæveautomatenDbContext>(options => options.UseSqlite("Data Source=Hæveautomaten.db"));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
