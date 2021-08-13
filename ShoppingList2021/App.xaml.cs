using System.Threading.Tasks;
using Prism;
using Prism.Ioc;
using ShoppingList2021.Database.Types;
using ShoppingList2021.ViewModels;
using ShoppingList2021.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ShoppingList2021
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await CreateOrMigrateDatabaseAsync();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        private static async Task CreateOrMigrateDatabaseAsync()
        {
            var container = ((App)Application.Current).Container;

            var dbServiceFactory = container.Resolve<IDbServiceFactory>();
            var dbService = dbServiceFactory.CreateNew();
            await dbService.CreateOrMigrateDatabaseAsync();

        }
    }
}
