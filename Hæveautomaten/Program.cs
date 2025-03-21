using Hæveautomaten.Data;
using Hæveautomaten.Controllers;
using Hæveautomaten.Repositories;
using Microsoft.EntityFrameworkCore;
using Hæveautomaten.Interfaces.Controllers;
using Hæveautomaten.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Hæveautomaten.Views;
using Hæveautomaten.Interfaces.Views;

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

            serviceCollection.AddSingleton<IBaseView, BaseView>();
            serviceCollection.AddSingleton<IMainController, MainController>();
            serviceCollection.AddSingleton<IBankController, BankController>();
            serviceCollection.AddSingleton<IAdminController, AdminController>();
            serviceCollection.AddSingleton<IPersonController, PersonController>();
            serviceCollection.AddSingleton<IAccountController, AccountController>();
            serviceCollection.AddSingleton<ICreditCardController, CreditCardController>();
            serviceCollection.AddSingleton<IAutomatedTellerMachineController, AutomatedTellerMachineController>();

            serviceCollection.AddSingleton<IAutomatedTellerMachineRepository, AutomatedTellerMachineRepository>();
            serviceCollection.AddSingleton<ICreditCardRepository, CreditCardRepository>();
            serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();
            serviceCollection.AddSingleton<IPersonRepository, PersonRepository>();
            serviceCollection.AddSingleton<IBankRepository, BankRepository>();

            serviceCollection.AddDbContext<HæveautomatenDbContext>(options => options.UseSqlite("Data Source=Data\\atm.db"));

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
