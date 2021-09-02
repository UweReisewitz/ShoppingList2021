using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database.Types;
using ShoppingList2021.Models;

namespace ShoppingList2021.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShoppingItemDetailViewModel : ViewModelBase
    {
        private readonly IMapper mapper;
        private readonly IPhoto photo;

        private IDbService dbService;
        private UIShoppingItem uiShoppingItem;

        public ShoppingItemDetailViewModel(INavigationService navigationService, IMapper mapper, IPhoto photo)
            : base(navigationService)
        {
            Title = "Detail View";
            this.mapper = mapper;
            this.photo = photo;

            UpdateSuggestedNames = new DelegateCommand<bool?>(async (bool? performUpdate) => await PerformUpdateSuggestedNamesAsync(performUpdate));
            TakePhoto = new DelegateCommand(async () => await TakePhotoAsync());
            PickPhoto = new DelegateCommand(async () => await PickPhotoAsync());
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        private ShoppingItemState itemState;
        public ShoppingItemState State
        {
            get => itemState;
            set
            {
                if (value != itemState)
                {
                    if (itemState == ShoppingItemState.Open)
                    {
                        LastBought = DateTime.Now;
                    }
                    itemState = value;
                }
            }
        }

        public IList<EnumValueDescription> ItemStateList => typeof(ShoppingItemState).ToList();

        [DependsOn(nameof(State))]
        public EnumValueDescription ItemState
        {
            get => new EnumValueDescription(State, State.GetDescription());
            set
            {
                if (value != null)
                {
                    State = (ShoppingItemState)value.EnumValue;
                }
            }
        }

        public DateTime LastBought { get; set; }


        public DelegateCommand<bool?> UpdateSuggestedNames { get; }
        private async Task PerformUpdateSuggestedNamesAsync(bool? performUpdate)
        {
            if (performUpdate.HasValue && performUpdate.Value)
            {
                SuggestedNames = await dbService.GetSuggestedNamesAsync(Name);
            }
            else
            {
                SuggestedNames = null;
            }
        }

        public List<string> SuggestedNames { get; private set; }

        public DelegateCommand TakePhoto { get; }
        private async Task TakePhotoAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                var imageData = await photo.TakePhoto();
                if (imageData == null)
                {
                    //await InteractionService.ShowAlertAsync(UIResources.GetString(202810),
                    //                                        UIResources.GetString(202811),
                    //                                        UIResources.GetString(UIShortcut.Remove, 10000)).ConfigureAwait(true);
                    IsBusy = false;
                    return;
                }

                ImageData = imageData;
                IsBusy = false;
            }
        }

        public DelegateCommand PickPhoto { get; }

        private async Task PickPhotoAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                var imageData = await photo.PickPhoto();
                if (imageData == null)
                {
                    //await InteractionService.ShowAlertAsync(UIResources.GetString(202810),
                    //                                        UIResources.GetString(202811),
                    //                                        UIResources.GetString(UIShortcut.Remove, 10000)).ConfigureAwait(true);
                    IsBusy = false;
                    return;
                }

                ImageData = imageData;
                IsBusy = false;
            }
        }

        public byte[] ImageData { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            dbService = (IDbService)parameters["DbService"];
            uiShoppingItem = (UIShoppingItem)parameters["Item"];
            mapper.Map(uiShoppingItem, this);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(Name) && uiShoppingItem.IsNewShoppingItem)
            {
                dbService.RemoveShoppingItem(uiShoppingItem.DbShoppingItem);
            }
            else
            {
                var oldItem = dbService.FindShoppingItem(Name);
                if (oldItem != null && oldItem.Id != Id)
                {
                    if (uiShoppingItem.IsNewShoppingItem)
                    {
                        dbService.RemoveShoppingItem(uiShoppingItem.DbShoppingItem);
                        LastBought = oldItem.LastBought;
                        ImageData = oldItem.ImageData;
                    }
                    uiShoppingItem = new UIShoppingItem(oldItem);
                    Id = uiShoppingItem.Id;
                }
                mapper.Map(this, uiShoppingItem);

                // this is not a good idea...
                // dbService.SaveChangesAsync().SafeFireAndForget();
                // instead this should by a synchronous call
                dbService.SaveChanges();
            }
        }
    }
}
