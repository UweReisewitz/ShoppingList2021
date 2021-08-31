using System;
using System.IO;
using PropertyChanged;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Types;
using Xamarin.Forms;

namespace ShoppingList2021.Models
{
    [AddINotifyPropertyChangedInterface]
    public class UIShoppingItem
    {
        public UIShoppingItem(IShoppingItem shoppingItem)
            : this(shoppingItem, false)
        {
        }

        public UIShoppingItem(IShoppingItem shoppingItem, bool isNewShoppingItem)
        {
            DbShoppingItem = shoppingItem;
            IsNewShoppingItem = isNewShoppingItem;
        }

        public bool IsNewShoppingItem { get; }

        public IShoppingItem DbShoppingItem { get; }
        public Guid Id
        {
            get => DbShoppingItem.Id;
            set => DbShoppingItem.Id = value;
        }
        public string Name
        {
            get => DbShoppingItem.Name;
            set => DbShoppingItem.Name = value;
        }
        public ShoppingItemState State
        {
            get => DbShoppingItem.State;
            set => DbShoppingItem.State = value;
        }
        public DateTime LastBought
        {
            get => DbShoppingItem.LastBought;
            set => DbShoppingItem.LastBought = value;
        }

        public byte[] ImageData
        {
            get => DbShoppingItem.ImageData;
            set => DbShoppingItem.ImageData = value;
        }

        [DependsOn(nameof(State))]
        public string StateText => State.GetDescription();

        [DependsOn(nameof(ImageData))]
        public ImageSource ImageItem => ImageSource.FromStream(() => new MemoryStream(ImageData));

        [DependsOn(nameof(State))]
        public bool IsOpen => State == ShoppingItemState.Open;

        [DependsOn(nameof(State))]
        public bool IsBought => State == ShoppingItemState.Bought;
    }
}
