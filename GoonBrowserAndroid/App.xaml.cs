namespace GoonBrowserAndroid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
/*
 * TODO features:
 * 
 * - WebView COMP
 * - Download COMP?
 * - Tabs COMP
 * - Inspect element
 * - Hold for actions popup WIP
 * - Ad-blocking
 * - Multi-language support
 * 
 * TODO issues:
 * This is where bugs are documented, I will switch over to using GitHub's shit when I implement the above features, and bugs will only be
 * documented on GitHub, so this will just be a reminder to LOOK AT GITHUB ISSUES FOR UR BUGS AND GLITCHES !Q!!!!!!!11
 * 
 */