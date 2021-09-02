using Prism;
using Prism.Ioc;
using ShoppingList2021.Core;
using ShoppingList2021.Core.Types;
using ShoppingList2021.Database;
using ShoppingList2021.Database.Types;
using ShoppingList2021.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace ShoppingList2021.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.SetFlags("SwipeView_Experimental");
            Forms.Init();
            LoadApplication(new ShoppingList2021.App(new WpfInitializer()));
        }
    }


    public class WpfInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPlatformSpecialFolder, PlatformSpecialFolder>();

            CommonDependencies.RegisterTypes(containerRegistry);
        }
    }
}
