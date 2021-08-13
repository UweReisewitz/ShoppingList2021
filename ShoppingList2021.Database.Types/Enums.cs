using System.ComponentModel;

namespace ShoppingList2021.Database
{
    public enum ShoppingItemState
    {
        [Description("Offen")]
        Open=1,
        [Description("Gekauft")]
        Bought=2,
        [Description("Einkauf abgeschlossen")]
        ShoppingComplete=3
    }
}
