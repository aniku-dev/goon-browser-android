using GoonBrowserAndroid.Views;

namespace GoonBrowserAndroid
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TabsPage), typeof(TabsPage));
            Routing.RegisterRoute(nameof(WebviewPage), typeof(WebviewPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}
