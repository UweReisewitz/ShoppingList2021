using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingList2021.Database.Types
{
    public interface IDbService
    {        
        Task CreateOrMigrateDatabaseAsync();

        Task SaveChangesAsync();

        Task<List<IShoppingItem>> GetShoppingListItemsAsync();

        Task AddShoppingItemAsync(IShoppingItem item);
        IShoppingItem CreateShoppingItem();
        void RemoveShoppingItem(IShoppingItem item);
        Task EndShoppingAsync();

        IShoppingItem FindShoppingItem(string name);
        Task<IShoppingItem> FindShoppingItemAsync(string name);

        Task<List<string>> GetSuggestedNamesAsync(string name);
    }
}
