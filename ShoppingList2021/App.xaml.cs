using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AutoMapper;
using Prism;
using Prism.AppModel;
using Prism.Events;
using Prism.Ioc;
using ShoppingList2021.Database.Types;
using ShoppingList2021.ViewModels;
using ShoppingList2021.Views;
using Xamarin.Forms;

namespace ShoppingList2021
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            InitializeApplicationAsync().SafeFireAndForget();
        }

        private async Task InitializeApplicationAsync()
        {
            await CreateOrMigrateDatabaseAsync();

            await NavigationService.NavigateAsync("NavigationPage/ShoppingItemsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IMapper>(new MapperConfiguration(cfg => ShoppingListMapperConfiguration.CreateMapping(cfg)).CreateMapper());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ShoppingItemsPage, ShoppingItemsViewModel>();
            containerRegistry.RegisterForNavigation<ShoppingItemDetailPage, ShoppingItemDetailViewModel>();

            // das sollte nicht nötig sein, ist ein Bug im aktuellen Prism Build
            // https://github.com/dansiegel/Prism.Container.Extensions/issues/200
            containerRegistry.RegisterSingleton<IKeyboardMapper, KeyboardMapper>();
        }

        protected override void OnNavigationError(INavigationError navigationError)
        {
#if DEBUG
            // Ensure we always break here while debugging!
            System.Diagnostics.Debugger.Break();
#endif

            base.OnNavigationError(navigationError);
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
