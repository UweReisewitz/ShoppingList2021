using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Model;

namespace ShoppingList2021.Database
{
    public class DbService : IDbService, IDisposable
    {
        private readonly IPlatformSpecialFolder platformSpecialFolder;
        private readonly LocalDbContext localContext;

        public DbService(IPlatformSpecialFolder platformSpecialFolder)
        {
            this.platformSpecialFolder = platformSpecialFolder;
            localContext = new LocalDbContext(platformSpecialFolder);
        }

        public async Task CreateOrMigrateDatabaseAsync()
        {
            using (var dbContext = new LocalDbContext(platformSpecialFolder))
            {
                await dbContext.CreateOrMigrateDatabaseAsync();
            }
        }

        public Task SaveChangesAsync()
        {
            return localContext.SaveChangesAsync();
        }

        public Task<List<IShoppingItem>> GetShoppingListItemsAsync()
        {
            return localContext.ShoppingItem
                .Where(si => si.State != ShoppingItemState.ShoppingComplete)
                .Cast<IShoppingItem>()
                .ToListAsync();
        }


        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (localContext != null)
                    {
                        localContext.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                isDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task AddShoppingItemAsync(IShoppingItem item)
        {
            await localContext.ShoppingItem.AddAsync((ShoppingItem)item);
        }

        public Task<List<string>> GetSuggestedNamesAsync(string name)
        {
            return localContext.ShoppingItem
                .Where(si => si.Name.StartsWith(name))
                .Select(si => si.Name)
                .ToListAsync();
        }

        public IShoppingItem FindShoppingItem(string name)
        {
            return localContext.ShoppingItem
                .Where(si => si.Name == name)
                .FirstOrDefault();
        }

        public Task<IShoppingItem> FindShoppingItemAsync(string name)
        {
            return localContext.ShoppingItem
                .Where(si => si.Name == name)
                .Cast<IShoppingItem>()
                .FirstOrDefaultAsync();
        }

        public void RemoveShoppingItem(IShoppingItem item)
        {
            localContext.Remove(item);
        }

        public async Task EndShoppingAsync()
        {
            var boughtItems = await localContext.ShoppingItem
                .Where(si => si.State == ShoppingItemState.Bought)
                .ToListAsync();

            foreach(var boughtItem in boughtItems)
            {
                boughtItem.State = ShoppingItemState.ShoppingComplete;
            }
            await localContext.SaveChangesAsync();
        }
    }
}
