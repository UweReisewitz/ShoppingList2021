using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Model;

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

            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(iSpecialFolder.ApplicationData, "ShoppingList.db3");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
