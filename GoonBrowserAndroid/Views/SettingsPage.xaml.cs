using GoonBrowserAndroid.ViewModels;

namespace GoonBrowserAndroid.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        BindingContext = settingsViewModel;
    }
}