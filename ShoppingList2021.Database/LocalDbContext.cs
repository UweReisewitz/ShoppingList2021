using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Model;
using ShoppingList2021.Database.Types;

[assembly: InternalsVisibleToAttribute("DataBaseMigrationHelper")]

namespace ShoppingList2021.Database
{
    internal class LocalDbContext : DbContext
    {
        public DbSet<ShoppingItem> ShoppingItem { get; set; }

        private readonly IPlatformSpecialFolder iSpecialFolder;

        public LocalDbContext(IPlatformSpecialFolder iSpecialFolder)
        {
            this.iSpecialFolder = iSpecialFolder;
        }

        public async Task CreateOrMigrateDatabaseAsync()
        {
            await Database.MigrateAsync();

            if (dbIsNew)
            {
                var item1 = new ShoppingItem()
                {
                    Name = "Äpfel",
                    State = ShoppingItemState.Open
                };
                var item2 = new ShoppingItem()
                {
                    Name = "Birnen",
                    State = ShoppingItemState.Open
                };
                var item3 = new ShoppingItem()
                {
                    Name = "Bananen",
                    State = ShoppingItemState.Bought
                };
                var item4 = new ShoppingItem()
                {
                    Name = "Müsli",
                    State = ShoppingItemState.ShoppingComplete,
                    LastBought = DateTime.Now
                };
                ShoppingItem.Add(item1);
                ShoppingItem.Add(item2);
                ShoppingItem.Add(item3);
                ShoppingItem.Add(item4);
                await SaveChangesAsync();
            }
        }

        private bool dbIsNew;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(iSpecialFolder.ApplicationData, "ShoppingList.db3");

            dbIsNew = !File.Exists(dbPath);

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
