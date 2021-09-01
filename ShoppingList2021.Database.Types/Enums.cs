using System.ComponentModel;

namespace ShoppingList2021.Database.Types
{
    public enum ShoppingItemState
    {
        [Description("Offen")]
        Open=0,
        [Description("Gekauft")]
        Bought=1,
        [Description("Einkauf abgeschlossen")]
        ShoppingComplete=2
    }
}
