using System;

namespace ShoppingList2021.Database
{
    public interface IShoppingItem
    {
        string Name { get; set; }
        ShoppingItemState State { get; set; }
        DateTime LastBought { get; set; }
        byte[] ImageData { get; set; }
    }
}
