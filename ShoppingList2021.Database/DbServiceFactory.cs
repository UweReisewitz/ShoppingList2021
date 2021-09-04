using Prism.Events;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Types;

namespace ShoppingList2021.Database
{
    public class DbServiceFactory : IDbServiceFactory
    {
        private readonly IPlatformSpecialFolder platformSpecialFolder;
        private readonly IEventAggregator eventAggregator;

        public DbServiceFactory(IPlatformSpecialFolder platformSpecialFolder, IEventAggregator eventAggregator)
        {
            this.platformSpecialFolder = platformSpecialFolder;
            this.eventAggregator = eventAggregator;
        }

        public IDbService CreateNew()
        {
            return new DbService(platformSpecialFolder, eventAggregator);
        }
    }
}
