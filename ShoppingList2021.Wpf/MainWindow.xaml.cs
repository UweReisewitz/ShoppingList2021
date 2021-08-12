using Prism;
using Prism.Ioc;
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

            Forms.Init();
            LoadApplication(new ShoppingList2021.App(new WpfInitializer()));
        }
    }


    public class WpfInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
