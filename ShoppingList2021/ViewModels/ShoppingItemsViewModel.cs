using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using ShoppingList2021.Database.Types;
using ShoppingList2021.Models;

namespace ShoppingList2021.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShoppingItemsViewModel : ViewModelBase
    {
        private readonly IDbService dbService;
        private readonly IPageDialogService pageDialogService;

        public ShoppingItemsViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDbServiceFactory dbServiceFactory)
            : base(navigationService)
        {
            this.dbService = dbServiceFactory?.CreateNew() ?? throw new ArgumentNullException(nameof(dbServiceFactory));
            this.pageDialogService = pageDialogService ?? throw new ArgumentNullException(nameof(pageDialogService));

            Title = "Shopping List 2021";

            Items = new ObservableCollection<UIShoppingItem>();
            LoadItemsCommand = new AsyncCommand(ExecuteLoadItemsCommandAsync);

            SetItemBought = new AsyncCommand<UIShoppingItem>(SetItemBoughtAsync);
            ItemTapped = new AsyncCommand<UIShoppingItem>(OnItemSelectedAsync);

            AddItemCommand = new AsyncCommand(OnAddItemAsync);
            ShoppingDoneCommand = new AsyncCommand(ShoppingDoneCommandAsync);
        }

        public AsyncCommand ShoppingDoneCommand { get; }
        private async Task ShoppingDoneCommandAsync()
        {
            await dbService.EndShoppingAsync();
            await ExecuteLoadItemsCommandAsync();
        }


        public AsyncCommand<UIShoppingItem> SetItemBought { get; }
        private async Task SetItemBoughtAsync(UIShoppingItem item)
        {
            if (item != null && item.State == ShoppingItemState.Open)
            {
                item.State = ShoppingItemState.Bought;
                item.LastBought = DateTime.Now;
                await dbService.SaveChangesAsync();
            }
        }


        public ObservableCollection<UIShoppingItem> Items { get; private set; }
        public AsyncCommand LoadItemsCommand { get; }
        public AsyncCommand AddItemCommand { get; }
        public AsyncCommand<UIShoppingItem> ItemTapped { get; }

        private async Task ExecuteLoadItemsCommandAsync()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await dbService.GetShoppingListItemsAsync();

                foreach (var item in items.OrderBy(i => i.State).ThenBy(i => i.Name))
                {
                    Items.Add(new UIShoppingItem(item));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private UIShoppingItem selectedItem;
        public UIShoppingItem SelectedItem
        {
            get => selectedItem;
            set => OnItemSelectedAsync(value).SafeFireAndForget();
        }

        private async Task OnAddItemAsync()
        {
            var dbShoppingItem = dbService.CreateShoppingItem();
            await dbService.AddShoppingItemAsync(dbShoppingItem);
            var uiShoppingItem = new UIShoppingItem(dbShoppingItem, true);

            await NavigateToDetailPageAsync(uiShoppingItem);
        }

        private async Task OnItemSelectedAsync(UIShoppingItem item)
        {
            selectedItem = item;
            if (item != null)
            {
                await NavigateToDetailPageAsync(item);
            }
        }

        private async Task NavigateToDetailPageAsync(UIShoppingItem uiShoppingItem)
        {
            var parameters = new NavigationParameters
                {
                    { "DbService", dbService },
                    { "Item", uiShoppingItem }
                };

            // This will push the ShoppingItemDetailPage onto the navigation stack
            await NavigationService.NavigateAsync("ShoppingItemDetailPage", parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                IsBusy = true;  // Refresh
            }
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            SelectedItem = null;

            await ExecuteLoadItemsCommandAsync();
        }
    }
}