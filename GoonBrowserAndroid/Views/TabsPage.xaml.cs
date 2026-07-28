using GoonBrowserAndroid.ViewModels;

namespace GoonBrowserAndroid.Views;

public partial class TabsPage : ContentPage
{
    public TabsPage(TabsViewModel tabsViewModel)
    {
        InitializeComponent();
        BindingContext = tabsViewModel;
    }
}