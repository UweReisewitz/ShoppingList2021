using Microsoft.EntityFrameworkCore.Design;
using ShoppingList2021.Core;
using ShoppingList2021.Database;

namespace DatabaseMigrationHelper
{
    internal class ShoppingListLocalDbContextFactory : IDesignTimeDbContextFactory<LocalDbContext>
    {
        public LocalDbContext CreateDbContext(string[] args)
        {
            return new LocalDbContext(new PlatformSpecialFolder());
        }
    }
}
