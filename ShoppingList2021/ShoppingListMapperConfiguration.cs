using System;
using AutoMapper;
using ShoppingList2021.Database.Types;
using ShoppingList2021.Models;
using ShoppingList2021.ViewModels;

namespace ShoppingList2021
{
    public class ShoppingListMapperConfiguration
    {
        public static void CreateMapping(IMapperConfigurationExpression configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.CreateMap<IShoppingItem, UIShoppingItem>().ReverseMap();
            configuration.CreateMap<UIShoppingItem, ShoppingItemDetailViewModel>().ReverseMap();
        }

    }
}
