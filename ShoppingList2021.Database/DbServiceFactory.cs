using ShoppingList2021.Core.Types;

namespace ShoppingList2021.Database
{
    public class DbServiceFactory : IDbServiceFactory
    {
        private readonly IPlatformSpecialFolder platformSpecialFolder;

        public DbServiceFactory(IPlatformSpecialFolder platformSpecialFolder)
        {
            this.platformSpecialFolder = platformSpecialFolder;
        }

        public IDbService CreateNew()
        {
            return new DbService(platformSpecialFolder);
        }
    }
}
