using System;
using System.IO;
using ShoppingList2021.Core.Types;

namespace ShoppingList2021.Core
{
    public class PlatformSpecialFolder : IPlatformSpecialFolder
    {
        public string ApplicationData
        {
            get
            {
                var retval = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ShoppingList");
                Directory.CreateDirectory(retval);
                return retval;
            }
        }
    }
}