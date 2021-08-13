using Prism.Ioc;
using ShoppingList2021.Database;
using ShoppingList2021.Database.Types;

namespace ShoppingList2021.Shared
{
    internal static class CommonDependencies
    {
        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<IDbServiceFactory, DbServiceFactory>();
        }
    }
}
